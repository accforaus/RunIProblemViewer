using System.Linq;
using Domain.Problem.Data;
using ProblemViewer.Scripts.Viewer.View.Base;

namespace ProblemViewer.Scripts.Viewer.View {
    public class ProblemViewMultiple : ProblemViewGridElement {
        public MultipleImage multipleImage = null;

        public override void Create(ProblemDataContent data, float size) {
            base.Create(data, size);
            if (dataContent.grids != null) {
                gridPanelController.CreateGrid(dataContent.grids);
            } else if (dataContent.table != null) {
                gridPanelController.CreateTable(dataContent.table.First());
            } else 
                gridPanel.SetActive(false);
        }

        protected override void SetImages() {
            dataContent.images.ForEach(image => imagePanelController.CreateImage(image));
            imagePanelController.SetMultipleCellSize(dataContent.images.First());
        }
    }
}