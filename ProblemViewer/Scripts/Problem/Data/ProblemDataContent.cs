using System;
using System.Collections.Generic;
using ProblemViewer.Scripts.Problem.Data;
using UnityEngine;

namespace Domain.Problem.Data {
    [Serializable]
    public class ProblemDataContent : BaseProblemData {
        [SerializeField] public GridData grids;
        [SerializeField] public List<TableData> table;

        public bool IsEmpty => content == "" && grids == null && table == null && images.Count == 0;
        
        public ProblemDataContent() {
            grids = null;
            table = null;
        }

        public ProblemDataContent(string content, List<ImageData> images, GridData grids, List<TableData> table) : base(content, images) {
            this.grids = grids;
            this.table = table;
        }

        public override void Base64ToSprite() {
            base.Base64ToSprite();
            grids?.Base64ToSprite();
            table?.ForEach(tb => tb.Base64ToSprite());
        }
    }
}