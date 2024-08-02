using Assets.Scripts.World;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Game.UI
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _loadingStateText;
        [SerializeField] private Slider _loadingSlider;

        private IWorld _world;

        [Inject]
        private void Construct(IWorld world)
        {
            _world = world;
        }

        private void Start()
        {
            _world.GenerateStageChanged += OnGenerateStageChanged;
            _world.GenerationCompleted += OnGenerationCompleted;
        }

        private void OnDestroy()
        {
            _world.GenerateStageChanged -= OnGenerateStageChanged;
            _world.GenerationCompleted -= OnGenerationCompleted;
        }

        private void OnGenerationCompleted()
        {
            gameObject.SetActive(false);
        }

        private void OnGenerateStageChanged(GenerateStage stage)
        {
            _loadingSlider.value += stage.Cost;
            _loadingStateText.text = stage.NameGeneration;
        }
    }
}
