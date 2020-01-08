using System;
using System.Collections.Generic;
using Domain.Problem.Data;
using UnityEngine;

namespace ProblemViewer.Scripts.Problem.Data {
    [Serializable]
    public abstract class BaseProblemData {
        [SerializeField] public string content;
        [SerializeField] public List<ImageData> images;
        
        protected BaseProblemData() {
            content = "";
            images = new List<ImageData>();
        }
        
        protected BaseProblemData(string content, List<ImageData> images) {
            this.content = content;
            this.images = images;
        }

        public virtual void Base64ToSprite() => images.ForEach(image => image.Base64ToSprite());
    }
}