using System;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerNetwork : NetworkBehaviour
    {
        private NetworkVariable<Vector3> _netPos = new(writePerm: NetworkVariableWritePermission.Owner);
        private NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);

        private void Update()
        {
            if (IsOwner)
            {
                _netPos.Value = transform.position;
                _netRot.Value = transform.rotation;
            }
            else
            {
                transform.position = _netPos.Value;
                transform.rotation = _netRot.Value;
            }
        }
    }
}