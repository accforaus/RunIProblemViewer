using System;
using System.Collections.Generic;
using ProblemViewer.Scripts.Problem.Data;
using UnityEngine;

namespace Domain.Problem.Data {
    /// <summary>
    /// Problem Grid Data
    /// </summary>
    [Serializable]
    public class GridData {
        /// <summary>
        /// List of Row Data
        /// </summary>
        [SerializeField] public List<RowData> rows;

        /// <summary>
        /// Constructor of Grid Data
        /// initialize row data
        /// </summary>
        public GridData() {
            rows = new List<RowData>();
        }

        /// <summary>
        /// Constructor of Grid Data
        /// initialize row data by parameters
        /// </summary>
        /// <param name="rows">data of row</param>
        public GridData(List<RowData> rows) {
            this.rows = rows;
        }

        /// <summary>
        /// Image to Sprite Methods
        /// </summary>
        public void Base64ToSprite() => rows.ForEach(row => row.Base64ToSprite());
    }

    [Serializable]
    public class RowData {
        [SerializeField] public List<ColData> cols;
        
        public RowData() {
            cols = new List<ColData>();
        }

        public RowData(List<ColData> cols) {
            this.cols = cols;
        }

        public void Base64ToSprite() => cols.ForEach(col => col.Base64ToSprite());
    }

    [Serializable]
    public class ColData : BaseProblemData {
        public ColData() { }

        public ColData(string content, List<ImageData> images) : base(content, images) { }
    }
}