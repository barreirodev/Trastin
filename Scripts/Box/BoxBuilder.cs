using UnityEngine;
using Landfill;
using Core;
using Misc;

namespace Box {
    public class BoxBuilder
    {
        private BoxMediator _prefab;
        private Vector3 _position;
        private BoxConfigurationDetails.Nature _nature;
        private OriginConfiguration _origin;
        private ContainerConfiguration _container;
        private Sprite _sprite;

        private EventBus _newBoxCreated;

        public BoxBuilder(EventBus newEvent)
        {
            _newBoxCreated = newEvent;
        }

        public enum Behaviour
        {
            OverHead,
            Disappear
        }
        private Behaviour _behaviour;

        public enum Movement
        {
            None,
            zMovement,
            xMovement
        }
        private Movement _movement;

        public BoxBuilder FromPrefab(BoxMediator prefab)
        {
            _prefab = prefab;
            return this;
        }

        public BoxBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public BoxBuilder WithBehaviour(Behaviour behaviour)
        {
            _behaviour = behaviour;
            return this;
        }

        public BoxBuilder WithMovement(Movement movement)
        {
            _movement = movement;
            return this;
        }

        public BoxBuilder WithNature(BoxConfigurationDetails.Nature nature)
        {
            _nature = nature;
            return this;
        }

        public BoxBuilder WithOrigin(OriginConfiguration origin)
        {
            _origin = origin;
            return this;
        }
        public BoxBuilder WithContainer(ContainerConfiguration container)
        {
            _container = container;
            return this;
        }

        public BoxBuilder WithSprite(Sprite sprite)
        {
            _sprite = sprite;
            return this;
        }

        private void AddBehaviour(BoxMediator box)
        {
            switch (_behaviour)
            {
                case Behaviour.OverHead:
                    box.gameObject.AddComponent<BoxOverHead>();
                    break;
                case Behaviour.Disappear:
                    box.gameObject.AddComponent<BoxDisappears>();
                    break;
                default:
                    throw new System.Exception("Behaviour not implemented");
            }
        }
        private void AddMovement(BoxMediator box)
        {
            switch (_movement)
            {
                case Movement.zMovement:
                    box.gameObject.AddComponent<LinearMovement>().SetMovement(new Vector3(0,0,1));
                    break;
                case Movement.xMovement:
                    box.gameObject.AddComponent<LinearMovement>().SetMovement(new Vector3(-1, 0, 0));
                    break;
            }
            box.gameObject.AddComponent<Destroyer>();
        }
        public BoxMediator Build(IObserver observer)
        {
            BoxMediator box = Object.Instantiate(_prefab, _position, Quaternion.identity);
            box.Configuration(_nature,_origin,_container,_sprite);
            AddBehaviour(box);
            AddMovement(box);

            box.Subscribe(observer);
            _newBoxCreated.NotifyEvent(box);
            
            return box;
        }
    }
}

