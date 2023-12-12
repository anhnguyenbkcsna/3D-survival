// using System;
// using Unity.Netcode;
// using UnityEngine;
//
// namespace Player
// {
//     public class PlayerNetwork : NetworkBehaviour
//     {
//         private readonly NetworkVariable<PlayerNetworkData> _netState =
//             new(writePerm: NetworkVariableWritePermission.Owner);
//         private Vector3 _velocity;
//         private float _rotationVelocity;
//
//         private void Update()
//         {
//             if (IsOwner)
//             {
//                 _netState.Value = new PlayerNetworkData()
//                 {
//                     position = transform.position,
//                     rotation = transform.rotation,
//                     isAttacking = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)
//                 };
//             }
//             else
//             {
//                 transform.position = Vector3.SmoothDamp(transform.position, _netState.Value.position, ref _velocity, 0.1f);
//                 transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _netState.Value.rotation.eulerAngles.y, ref _rotationVelocity, 0.1f), 0);
//             }
//         }
//
//         struct PlayerNetworkData : INetworkSerializable
//         {
//             public Vector3 position;
//             public Quaternion rotation;
//             public bool isAttacking;
//
//             internal Vector3 Position
//             {
//                 get => position;
//                 set => position = value;
//             }
//             internal Vector3 Rotation
//             {
//                 get => rotation.eulerAngles;
//                 set => rotation = Quaternion.Euler(value);
//             }
//
//             public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
//             {
//                 serializer.SerializeValue(ref position);
//                 serializer.SerializeValue(ref rotation);
//                 serializer.SerializeValue(ref isAttacking);
//             }
//         }
//     }
// }