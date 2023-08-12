using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public abstract class SkillController : MonoBehaviour
{
    protected GameObject _player = null;
    public Define.Skills weaponType;

    PlayerStat _playerStat;
    WeaponStats _weaponStatGetter;
    public List<Dictionary<string, int>> _weaponStat = new List<Dictionary<string, int>>();


    public int _damage = 1;
    public float _speed = 1;
    public int _level = 1;
    public int _countPerCreate = 1;
    public float _cooldown = 1;
    public int _size = 1;


    public abstract int _weaponType { get; }

    private void Start()
    {
        _player = GameManager.Instance.playerObject;
        _playerStat = GameManager.Instance.player.GetComponent<PlayerStat>();
        _weaponStatGetter = GameManager.Instance.weaponStats;
        if (weaponType == Define.Skills.Korand) return;
        _weaponStat = _weaponStatGetter.StatInitialize(weaponType);
        for (int i = 0; i < 5; i++)
        {
            foreach (var kv in _weaponStat[i])
            {
                Debug.LogFormat("{0} : {1}", kv.Key, kv.Value);
            }
        }
    }

    public virtual void WeaponLevelUp()
    {
        if (_level > 5)
            _level = 5;

        _damage = _weaponStat[_level - 1]["damage"];
        _countPerCreate = _weaponStat[_level - 1]["countPerCreate"];
        if (weaponType == Define.Skills.Koran)
        {
            _size = _weaponStat[_level - 1]["size"];
            transform.localScale = new Vector3(_size * 7, _size * 7, _size * 7);
        }

        //_damage = (int)(_weaponStat[_level].damage * ((float)(100 + _playerStat.Damage) / 100f));
        //_movSpeed = _weaponStat[_level].movSpeed;
        //_force = _weaponStat[_level].force;
        //_cooldown = _weaponStat[_level].cooldown * (100f / (100f + _playerStat.Cooldown));
        //_size = _weaponStat[_level].size;
        //_penetrate = _weaponStat[_level].penetrate;
        //_countPerCreate = _weaponStat[_level].countPerCreate + _playerStat.Amount;
    }


}
