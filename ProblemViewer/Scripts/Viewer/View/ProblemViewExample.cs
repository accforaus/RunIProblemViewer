using System.Linq;
using Domain.Problem.Data;
using ProblemViewer.Scripts.Viewer.View.Base;
using UnityEngine;

namespace ProblemViewer.Scripts.Viewer.View {
    public class ProblemViewExample : ProblemViewGridElement {
        [SerializeField] private bool isBox = false;

        public override void Create(ProblemDataContent data, float size) {
            base.Create(data, size);
            if (dataContent.grids != null) {
                gridPanelController.CreateGrid(dataContent.grids);
            } else if (dataContent.table != null) {
                gridPanelController.CreateTable(dataContent.table.First());
            } else 
                gridPanel.SetActive(false);
        }
    }
}