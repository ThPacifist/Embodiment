// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Control/Air/AirControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @AirControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @AirControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""AirControls"",
    ""maps"": [
        {
            ""name"": ""AirMovement"",
            ""id"": ""e8482355-1338-46dd-bfd9-23dfa8158db6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""b1fea356-6c9b-48ff-af4c-61c3092fadeb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""1d5edd3b-7271-419e-a659-93fa74275c6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Embody"",
                    ""type"": ""Button"",
                    ""id"": ""26900589-21b4-4377-b41c-1a3295366358"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fly"",
                    ""type"": ""Button"",
                    ""id"": ""9f2222e9-82fa-4a5d-9d68-bbf113a944be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special"",
                    ""type"": ""Button"",
                    ""id"": ""5851cb45-7ad6-44db-a3f9-1199ff20b6dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""134cb187-2b85-4aea-ba23-ffa9fe35a344"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""487825e4-61a7-4c72-b5d4-20519d086476"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a62c6db0-58e3-4fcc-80dd-362fcbb15d84"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5ca832f5-c507-4b19-bf92-5dab9c276d56"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controller"",
                    ""id"": ""fc8a6dda-4795-4492-9f80-a05bdf1f7868"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""40576a88-cd42-49a2-a564-aa4ca6def483"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""273be5dd-28bd-4f06-9662-41e3cece2c8b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5e7f9bc1-2a35-4ee6-9532-ff87c2f9d681"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a06ded12-d801-4c3e-bf56-0b3901c6fc64"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7ce9de8-a87c-4b53-91b4-38f37d992854"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e7ebb60-f70e-4107-ab69-343357813354"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f11325f5-fd58-4c44-8657-0d7bde4a0fd8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f4b23f0-1059-43be-9423-70884ab7f05d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c9dd0da-ec4a-44a0-8ab1-94c5f0705f26"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Embody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3db49437-52ee-46d1-8430-80cb10edb922"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Embody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d7ec30a-0542-4784-94ee-8bba03382eef"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19b9341c-c4c2-4688-bdd8-2f3ccdc60af0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fly"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // AirMovement
        m_AirMovement = asset.FindActionMap("AirMovement", throwIfNotFound: true);
        m_AirMovement_Movement = m_AirMovement.FindAction("Movement", throwIfNotFound: true);
        m_AirMovement_Interact = m_AirMovement.FindAction("Interact", throwIfNotFound: true);
        m_AirMovement_Embody = m_AirMovement.FindAction("Embody", throwIfNotFound: true);
        m_AirMovement_Fly = m_AirMovement.FindAction("Fly", throwIfNotFound: true);
        m_AirMovement_Special = m_AirMovement.FindAction("Special", throwIfNotFound: true);
        m_AirMovement_Pause = m_AirMovement.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // AirMovement
    private readonly InputActionMap m_AirMovement;
    private IAirMovementActions m_AirMovementActionsCallbackInterface;
    private readonly InputAction m_AirMovement_Movement;
    private readonly InputAction m_AirMovement_Interact;
    private readonly InputAction m_AirMovement_Embody;
    private readonly InputAction m_AirMovement_Fly;
    private readonly InputAction m_AirMovement_Special;
    private readonly InputAction m_AirMovement_Pause;
    public struct AirMovementActions
    {
        private @AirControls m_Wrapper;
        public AirMovementActions(@AirControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_AirMovement_Movement;
        public InputAction @Interact => m_Wrapper.m_AirMovement_Interact;
        public InputAction @Embody => m_Wrapper.m_AirMovement_Embody;
        public InputAction @Fly => m_Wrapper.m_AirMovement_Fly;
        public InputAction @Special => m_Wrapper.m_AirMovement_Special;
        public InputAction @Pause => m_Wrapper.m_AirMovement_Pause;
        public InputActionMap Get() { return m_Wrapper.m_AirMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AirMovementActions set) { return set.Get(); }
        public void SetCallbacks(IAirMovementActions instance)
        {
            if (m_Wrapper.m_AirMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnInteract;
                @Embody.started -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnEmbody;
                @Embody.performed -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnEmbody;
                @Embody.canceled -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnEmbody;
                @Fly.started -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnFly;
                @Fly.performed -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnFly;
                @Fly.canceled -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnFly;
                @Special.started -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnSpecial;
                @Special.performed -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnSpecial;
                @Special.canceled -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnSpecial;
                @Pause.started -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_AirMovementActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_AirMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Embody.started += instance.OnEmbody;
                @Embody.performed += instance.OnEmbody;
                @Embody.canceled += instance.OnEmbody;
                @Fly.started += instance.OnFly;
                @Fly.performed += instance.OnFly;
                @Fly.canceled += instance.OnFly;
                @Special.started += instance.OnSpecial;
                @Special.performed += instance.OnSpecial;
                @Special.canceled += instance.OnSpecial;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public AirMovementActions @AirMovement => new AirMovementActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IAirMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnEmbody(InputAction.CallbackContext context);
        void OnFly(InputAction.CallbackContext context);
        void OnSpecial(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
