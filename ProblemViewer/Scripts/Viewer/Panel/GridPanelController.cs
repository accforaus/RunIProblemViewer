using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Problem.Data;
using ProblemViewer.Scripts.Problem.Data;
using ProblemViewer.Scripts.Viewer.Item;
using UnityEngine;
using UnityEngine.UI;

namespace ProblemViewer.Scripts.Viewer.Panel {
    public class GridLayoutType {
        public bool onlyText;
        public bool onlyImage;
        public bool both;
    }
    public class GridPanelController : GridLayoutController {
        [SerializeField] private GameObject gridPanelItemPrefab;

        private int cellCount;
        private int rowCount;
        private List<GridPanelItemController> items = new List<GridPanelItemController>();
        private TableData tableData = null;
        private GridData gridData = null;

        public void CreateTable(TableData tableData, float fontSize = 38f) {
            this.tableData = tableData;
            cellCount = tableData.rows.First().cells.Count;
            rowCount = tableData.rows.Count;
            grid.spacing = Vector2.zero;
            tableData.rows.ForEach(row => {
                row.cells.ForEach(cell => {
                    var layoutType = new GridLayoutType();
                    if (cell.content != "" && cell.images.Count > 0) {
                        layoutType.both = true;
                    } else if (cell.content == "" && cell.images.Count > 0) {
                        layoutType.onlyImage = true;
                    } else if (cell.content != "" && cell.images.Count == 0) {
                        layoutType.onlyText = true;
                    }
                    CreateContent(cell, fontSize, true, layoutType);
                });
            });
            
            SetDynamicGrid();
        }
        
        public void CreateGrid(GridData gridData, float fontSize = 38f) {
            this.gridData = gridData;
            cellCount = gridData.rows.First().cols.Count;
            rowCount = gridData.rows.Count;
            grid.spacing = Vector2.zero;
            gridData.rows.ForEach(row => {
                row.cols.ForEach(col => {
                    var layoutType = new GridLayoutType();
                    if (col.content != "" && col.images.Count > 0) {
                        layoutType.both = true;
                    } else if (col.content == "" && col.images.Count > 0) {
                        layoutType.onlyImage = true;
                    } else if (col.content != "" && col.images.Count == 0) {
                        layoutType.onlyText = true;
                    }
                    CreateContent(col, fontSize, false, layoutType);
                });
            });
            
            SetDynamicGrid();
        }

        private void CreateContent(BaseProblemData data, float fontSize, bool isTable, GridLayoutType layoutType) {
            var item = Instantiate(gridPanelItemPrefab, transform.position, Quaternion.identity);
            var controller = item.GetComponent<GridPanelItemController>();
            controller.CreateContent(data, fontSize, isTable);
            controller.layoutType = layoutType;
            item.transform.SetParent(transform);
            item.SetActive(true);
            var itemRect = item.GetComponent<RectTransform>();
            itemRect.localScale = Vector3.one;
            items.Add(controller);
        }

        private void SetDynamicGrid() {
            StartCoroutine(SetCellSize(width => {
                Vector2 cellSize;
                var hasImage = items.Any(controller => controller.layoutType.both || controller.layoutType.onlyImage);
                if (hasImage) {
                    var hasSomeText = items.Any(controller => controller.layoutType.onlyImage);
                    float height;
                    if (hasSomeText)
                        height = items.Select(controller => controller.SetCellSize(width / cellCount)).Min();
                    else
                        height = items.Select(controller => controller.SetCellSize(width / cellCount)).Max();
                    cellSize = new Vector2(width / cellCount, height * 1.2f);
                } else {
                    if (rowCount == 1 && cellCount > 3) {
                        var maxContentLength = GetMaxContentSize();
                        cellSize = maxContentLength > 8 ? new Vector2(width / 2, 100.0f) : new Vector2(width / 3, 100.0f);
                    } else {
                        if (rowCount == 2 && cellCount == 3) {
                            cellSize = HasContentLengthMultipleRow(10) ? new Vector2(width / cellCount, 150f) : new Vector2(width / cellCount, 100f);
                        } else
                            cellSize = new Vector2(width / cellCount, 100f);
                    }
                }

                grid.cellSize = cellSize;
            }));
        }

        private int GetMaxContentSize() {
            return tableData?.rows.First().cells.Select(cell => cell.content.Length).Max() ?? gridData.rows.First().cols.Select(col => col.content.Length).Max();
        }

        private bool HasContentLengthMultipleRow(int length) {
            if (tableData != null) {
                return tableData.rows.SelectMany(row => row.cells)
                    .Any(cell => cell.content.Length >= length);
            }
            else return gridData.rows.SelectMany(row => row.cols)
                    .Any(col => col.content.Length >= length);
        }

        private void SetImageSize(float width, float height) {
            items.ForEach(item => {
                var gridRect = item.GetComponent<GridPanelItemController>()
                    .ImageContent
                    .GetComponent<RectTransform>();
                
                gridRect.sizeDelta = new Vector2(width, height);
            });
        }
    }
}