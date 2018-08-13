using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : ItemController {
    
    /// <summary>
    /// Release the object at the specified position
    /// </summary>
    /// <param name="position"></param>
    public new void ReleaseAt(Vector2 position)
    {
        base.ReleaseAt(position);
    }
}
