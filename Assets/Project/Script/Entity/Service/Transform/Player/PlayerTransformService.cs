using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class PlayerTransformService :
        AbstractMovingTransformService<PlayerTransformServiceData>, IPlayerTransformService
    {
        private readonly ITranslationService translationService;

        public override Quaternion Orientation => Quaternion.Euler(0.0f, Yaw, 0.0f);

        [Inject]
        public PlayerTransformService(ITranslationService translationService, Scene scene) :
            base(scene)
        {
            this.translationService = translationService;
        }

        public void Move(Vector2 translationDirection)
        {
            var translationDirection1 =
                new Vector3(translationDirection.x, 0.0f, translationDirection.y);

            Position = translationService.Translate(Position, Orientation, translationDirection1,
                MaxMovementSpeed);
        }
    }
}
