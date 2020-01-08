using System;
using Domain.Problem.Data;
using ProblemViewer.Scripts.Viewer.Panel;
using UnityEngine;
using UnityEngine.UI;

namespace ProblemViewer.Scripts.Viewer.View.Base {
    public enum MultipleIsOneImageLocation {
        Question, Multiple
    }
    
    public class ProblemViewElement : MonoBehaviour {
        [SerializeField] protected ImagePanelController imagePanelController;
        [SerializeField] protected TEXDraw tex;
        [SerializeField] protected GameObject contentPanel;
        [SerializeField] protected GameObject imagePanel;

        protected ProblemDataContent dataContent;
        
        public virtual void Create(ProblemDataContent data, float size) {
            dataContent = data;
            if (data.content == "") contentPanel.SetActive(false);
            else {
                tex.text = data.content;
                tex.size = size;
            }
            if (dataContent.images.Count > 0) {
                SetImages();
            } else imagePanel.SetActive(false);
        }

        protected virtual void SetImages() {
            dataContent.images.ForEach(image => imagePanelController.CreateImage(image));
            imagePanelController.SetCellSize(dataContent.images, () => { });
        }
    }
}