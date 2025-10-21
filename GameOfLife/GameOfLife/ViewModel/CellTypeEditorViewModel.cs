using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;
using GameOfLife.Model;

namespace GameOfLife.ViewModel;

public class CellTypeEditorViewModel : ObservableObject
{
    private static CellTypeEditorViewModel? _instance;
    public static CellTypeEditorViewModel Instance => _instance ??= new CellTypeEditorViewModel();

    public ObservableCollection<CellType> CellTypes { get; } = new();

    private CellType? _selectedCellType;
    public CellType? SelectedCellType
    {
        get => _selectedCellType;
        set => Set(ref _selectedCellType, value);
    }

    private CellTypeEditorViewModel()
    {
        for (int i = 0; i < CellTypeRegistry.Count(); i++)
        {
            var cell = CellTypeRegistry.Get(i);
            cell.PropertyChanged += Cell_PropertyChanged;
            CellTypes.Add(cell);
        }
    }

    private void AddNewCellType()
    {
        int newId = CellTypes.Count; 
        var newCell = new CellType(newId, "", "", Color.FromArgb(0, 10, 110), $"Cell {newId}");
        newCell.PropertyChanged += Cell_PropertyChanged;
        CellTypes.Add(newCell);
        SelectedCellType = newCell;

        CellTypeRegistry.Register(newCell);
    }
    private void Cell_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CellType.IsPriority))
        {
            var changedCell = sender as CellType;
            if (changedCell == null) return;

            if (changedCell.IsPriority)
            {
                // Сбрасываем IsPriority у всех остальных клеток
                foreach (var cell in CellTypes)
                {
                    if (cell != changedCell && cell.IsPriority)
                        cell.IsPriority = false;
                }

                SelectedCellType = changedCell;
                MainViewModel.Instance.MainCellType = SelectedCellType;
            }
            else
            {
                if (SelectedCellType == changedCell)
                    SelectedCellType = null;
            }
        }
    }

    public ICommand AddNewCellCommand => new RelayCommand(AddNewCellType);
}