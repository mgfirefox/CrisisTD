using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class Scene
    {
        public SceneLoop Loop { get; }
        public SceneSettings Settings { get; }

        [Inject]
        public Scene(SceneLoop loop, SceneSettings settings)
        {
            Loop = loop;
            Settings = settings;
        }
    }
}
