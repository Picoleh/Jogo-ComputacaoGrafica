using UnityEngine;

public interface ISaveable{
    object GetData();
    void SetData(object data);
}
