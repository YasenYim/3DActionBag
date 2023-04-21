using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {

    public override void OnDie()
    {
        base.OnDie();

        // 掉落道具
        ItemData item = WorldItemManager.Instance.CreateItem(Random.Range(1001, 1005));
        ModelInfo prefab = Resources.Load<ModelInfo>(item.Json.modelPath);

        // 创建一个随机道具
        ModelInfo model = Instantiate(prefab, transform.position, Quaternion.identity);
        model.itemData = item;
    }
}
