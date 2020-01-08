using Domain.Problem.Data;
using ProblemViewer.Scripts.Viewer.Panel;
using ProblemViewer.Scripts.Viewer.View.Base;
using UnityEngine;

namespace ProblemViewer.Scripts.Viewer.View {
    public class ProblemViewQuestion : ProblemViewElement {
        [SerializeField] protected ImagePanelController multipleImageController;
        [SerializeField] protected GameObject multipleImagePanel;
        public MultipleImage multipleImage = null;
        
        public override void Create(ProblemDataContent data, float size) {
            base.Create(data, size);
            if (multipleImage == null || multipleImage.multipleLocation == MultipleLocation.Multiple) 
                multipleImagePanel.SetActive(false);
        }
    }
}