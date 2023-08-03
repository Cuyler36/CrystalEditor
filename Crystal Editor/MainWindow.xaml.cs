using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CrystalEditor.Core;
using CrystalEditor.Core.NPCs;
using CrystalEditor.Core.World;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace CrystalEditor.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Save _save;
        private Hero _selectedHero = null;
        private int _langId = 0;
        private string _saveFolderPath = "";

        public MainWindow()
        {
            InitializeComponent();
            PopulateOpenMenu();
        }

        private void RefreshData()
        {
            GilNumericBox.Text = _save.Gil.ToString();
            ElementiteNumericBox.Text = _save.Elementite.ToString();
            CountryTextBox.Text = _save.CountryName;
            PlayerTextBox.Text = _save.PlayerName;

            MedalsDataGrid.ItemsSource = _save.Medals;
            FlagDataGrid.ItemsSource = _save.Flags.AsList();
            ConstructionsDataGrid.ItemsSource = _save.City.Constructions;

            /* Villager Data */
            VillagerListBox.Items.Clear();
            foreach (var item in _save.Villagers.Select(h => new ComboBoxItem { Content = $"{Resources["Villager"]} #{VillagerListBox.Items.Count + 1} [{h.Name}]" }))
            {
                VillagerListBox.Items.Add(item);
            }
            VillagerListBox.SelectedIndex = 0;

            /* Hero Data */
            HeroListBox.Items.Clear();
            foreach (var item in _save.Heros.Select(h => new ComboBoxItem { Content = $"{Resources["Hero"]} #{HeroListBox.Items.Count + 1} [{h.Name}]" }))
            {
                HeroListBox.Items.Add(item);
            }
            HeroListBox.SelectedIndex = 0;

            /* Dungeon Data */
            DungeonListBox.Items.Clear();
            foreach (var item in _save.Dungeons.Select(h => new ComboBoxItem { Content = $"{h.Name}" }))
            {
                DungeonListBox.Items.Add(item);
            }
            DungeonListBox.SelectedIndex = 0;

            /* Wandering Monster Data */
            WanderingListBox.Items.Clear();
            foreach (var item in _save.WanderingMonsters.Select(h => new ComboBoxItem { Content = $"{h.Name}" }))
            {
                WanderingListBox.Items.Add(item);
            }
            WanderingListBox.SelectedIndex = 0;

            GilNumericBox.IsEnabled = true;
            ElementiteNumericBox.IsEnabled = true;
            CountryTextBox.IsEnabled = true;
            PlayerTextBox.IsEnabled = true;
        }

        private void OpenMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is MenuItem item) || item.Tag == null) return;
            Title = $"Crystal Editor - {item.Header as string}";
            string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dolphin Emulator/Wii/title/00010001/57464345/data", item.Tag as string);
            _save = new Save(savePath);
            //OpenItem.IsEnabled = false;
            RefreshData();
            SaveItem.IsEnabled = true;
            DumpItem.IsEnabled = true;
        }

        private void OpenManuallyMenuItemClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            
            if (fileDialog.ShowDialog() == true)
            {
                _saveFolderPath = Path.GetDirectoryName(fileDialog.FileName);
                PopulateOpenMenu();
            }
        }

        private void SetNameFromSlot(MenuItem item, SaveSlot slot)
        {
            ResourceDictionary dict = Resources.MergedDictionaries[_langId];
            item.Resources.MergedDictionaries.Add(dict);
            string name = slot.CountryName;
            if (slot.Days > -1)
            {
                name += $" ({slot.Days} {dict["Days"]})";
            }
            if (slot.End)
            {
                name += $" [{dict["Defeated"]}]";
            }
            item.Header = name;
        }

        private void PopulateOpenMenu()
        {
            OpenItem.Items.Clear();

            // Load data in
            if (string.IsNullOrEmpty(_saveFolderPath))
            {
                _saveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dolphin Emulator/Wii/title/00010001/57464345/data");
            }
            
            if (!Directory.Exists(_saveFolderPath)) return;

            // Load Arch
            string archFilePath = Path.Combine(_saveFolderPath, "ARCH.SAV");
            if (!File.Exists(archFilePath)) return;
            Arch archFile = new Arch(archFilePath);
            IReadOnlyList<SaveSlot> list = archFile.GetSlots();
            for (int i = 0; i < list.Count; i++)
            {
                MenuItem item = new MenuItem { Tag = $"DAT{i}.SAV" };
                if (list[i].Current)
                {
                    item.Icon = new Image { Source = new BitmapImage(new Uri("/Resources/star.png", UriKind.Relative)) }; // pack://application:,,,/Crystal Editor;component
                }
                else if (list[i].End)
                {
                    item.Icon = new Image { Source = new BitmapImage(new Uri("/Resources/skull.png", UriKind.Relative)) };
                }

                SetNameFromSlot(item, list[i]);
                item.Click += OpenMenuItemClick;
                OpenItem.Items.Add(item);
            }

            // Add default
            MenuItem openSelect = new MenuItem
            {
                Header = Resources.MergedDictionaries[_langId]["OpenManually"]
            };

            openSelect.Click += OpenManuallyMenuItemClick;
            OpenItem.Items.Add(openSelect);

            OpenItem.IsEnabled = true;
        }

        private void OnSaveMenuItemClick(object sender, RoutedEventArgs e)
        {
            _save?.Write();
        }

        private void OnGilTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!uint.TryParse(GilNumericBox.Text, out uint gil))
            {
                e.Handled = true;
                return;
            }

            _save.Gil = gil;
        }

        private void OnElementiteTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!uint.TryParse(ElementiteNumericBox.Text, out uint elementite))
            {
                e.Handled = true;
                return;
            }

            _save.Elementite = elementite;
        }

        private void CountryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _save.CountryName = CountryTextBox.Text;
        }

        private void PlayerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _save.PlayerName = PlayerTextBox.Text;
        }

        private void HeroListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HeroListBox.SelectedIndex < 0) return;
            Hero hero = _save.Heros[HeroListBox.SelectedIndex];

            HeroPropertyGrid.SelectedObject = hero;
            CombatantPropertyGrid.SelectedObject = hero.Combatant;
            InventoryDataGrid.ItemsSource = hero.Combatant.Inventory.Items;

            HeroPropertyGrid.SelectedObjectTypeName = hero is TravellerHero ? "Traveller Hero" : "Hero";

            _selectedHero = hero;
        }

        private void HeroPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (e.OriginalSource is PropertyItem propertyItem && HeroPropertyGrid.SelectedObject is Hero hero)
            {
                string propertyTitle = propertyItem.DisplayName;

                if (propertyTitle == "Name")
                {
                    int idx = _save.Heros.IndexOf(hero);

                    if (idx != -1 && HeroListBox.Items[idx] is ComboBoxItem item)
                    {
                        item.Content = $"{Resources["Hero"]} #{idx + 1} [{hero.Name}]";
                    }
                }
            }
        }

        private void VillagerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VillagerListBox.SelectedIndex < 0) return;
            Villager villager = _save.Villagers[VillagerListBox.SelectedIndex];

            VillagerPropertyGrid.SelectedObject = villager;
        }

        private void VillagerPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (e.OriginalSource is PropertyItem propertyItem && VillagerPropertyGrid.SelectedObject is Villager villager)
            {
                string propertyTitle = propertyItem.DisplayName;

                if (propertyTitle == "Name")
                {
                    /* Do nothing for now */
                }
            }
        }

        private void DungeonListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DungeonListBox.SelectedIndex < 0) return;
            Dungeon dungeon = _save.Dungeons[DungeonListBox.SelectedIndex];

            DungeonPropertyGrid.SelectedObject = dungeon;
        }

        private void DungeonPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (e.OriginalSource is PropertyItem propertyItem && DungeonPropertyGrid.SelectedObject is Dungeon dungeon)
            {

            }
        }

        private void WanderingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WanderingListBox.SelectedIndex < 0) return;
            WanderingMonster wanderer = _save.WanderingMonsters[WanderingListBox.SelectedIndex];

            WanderingPropertyGrid.SelectedObject = wanderer;
        }

        private void WanderingPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (e.OriginalSource is PropertyItem propertyItem && DungeonPropertyGrid.SelectedObject is Dungeon dungeon)
            {

            }
        }

        private void DumpItem_Click(object sender, RoutedEventArgs e)
        {
            _save?.DumpJSON($"{_saveFolderPath}/dump.json");
        }

        private void DecompressItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Compressed files (*.press)|*.press|Binary files (*.bin)|*.bin|All files (*.*)|*.*";

            if (fd.ShowDialog() == true)
            {
                string file = fd.FileName;
                string output_file = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file));

                List<byte[]> files = Decompression.Decompress(File.ReadAllBytes(file));

                for (int i = 0; i < files.Count; i++)
                {
                    byte[] data = files[i];
                    File.WriteAllBytes($"{output_file}_file{i}.bin", data);
                }
            }
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedHero != null)
            {
                Inventory inv = _selectedHero.Combatant.Inventory;

                if (inv.Items.Count < 25)
                {
                    inv.AddItem(ItemType.WEAPON_SWORD, 1);
                }
            }
        }

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedHero != null)
            {
                Inventory inv = _selectedHero.Combatant.Inventory;

                if (inv.Items.Count > 0 && InventoryDataGrid.SelectedIndex != -1)
                {
                    inv.RemoveItem(InventoryDataGrid.SelectedIndex);
                }
            }
        }
    }
}
