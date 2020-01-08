using System;
using System.Collections.Generic;
using ProblemViewer.Scripts.Problem.Data;
using UnityEngine;

namespace Domain.Problem.Data {
    [Serializable]
    public class TableData {
        [SerializeField] public List<TableRow> rows;

        public TableData() {
            rows = new List<TableRow>();
        }

        public TableData(List<TableRow> rows) {
            this.rows = rows;
        }

        public void Base64ToSprite() => rows.ForEach(row => row.Base64ToSprite());
    }


    [Serializable]
    public class TableRow {
        [SerializeField] public List<TableCell> cells;

        public TableRow() {
            cells = new List<TableCell>();
        }

        public TableRow(List<TableCell> cells) {
            this.cells = cells;
        }

        public void Base64ToSprite() => cells.ForEach(cell => cell.Base64ToSprite());
    }

    [Serializable]
    public class TableCell : BaseProblemData {
        public TableCell() { }

        public TableCell(string content, List<ImageData> images) : base(content, images) { }
    }
}