# About NavMesh Obstacles

![On the left, carving isn't turned on, and the agent tries to steer around the obstacle. When carving is turned on, the agent plans a route around the blocked location.](./Images/NavMeshObstacleCarving.svg)

NavMesh Obstacles can affect the NavMesh Agent’s navigation during the game in two ways:

## Obstructing

When **Carve** is not enabled, the default behavior of the NavMesh Obstacle is similar to that of a [**Collider**][1]. NavMesh Agents try to avoid [**collisions**][2] with the NavMesh Obstacle, and when close, they collide with the NavMesh Obstacle. Obstacle avoidance behavior is very basic, and has a short radius. As such, the NavMesh Agent might not be able to find its way around in an environment cluttered with NavMesh Obstacles. This mode is best used in cases where the obstacle is constantly moving (for example, a vehicle or player character).

## Carving

When **Carve** is enabled, the obstacle carves a hole in the NavMesh when stationary. When moving, the obstacle is an obstruction. When a hole is carved into the NavMesh, the pathfinder is able to navigate the NavMesh Agent around locations cluttered with obstacles, or find another route if the current path gets blocked by an obstacle. It’s good practice to turn on carving for NavMesh Obstacles that generally block navigation but can be moved by the player or other game events like explosions (for example, crates or barrels).

![An agent trying to navigate a cluttered environment with carving disabled](./Images/NavMeshObstacleTrap.svg)

## Logic for moving NavMesh Obstacles

Unity treats the NavMesh Obstacle as moving when it has moved more than the distance set by the **Carve** > **Move Threshold**. When the NavMesh Obstacle moves, the carved hole also moves. However, to reduce CPU overhead, the hole is only recalculated when necessary. The result of this calculation is available in the next frame update. The recalculation logic has two options:

- Only carve when the NavMesh Obstacle is stationary

- Carve when the NavMesh Obstacle has moved

### Only carve when the NavMesh Obstacle is stationary

This is the default behavior. To enable it, tick the NavMesh Obstacle component’s **Carve Only Stationary** checkbox. In this mode, when the NavMesh Obstacle moves, the carved hole is removed. When the NavMesh Obstacle has stopped moving and has been stationary for more than the time set by **Carving Time To Stationary**, it is treated as stationary and the carved hole is updated again. While the NavMesh Obstacle is moving, the NavMesh Agents avoid it using collision avoidance, but don’t plan paths around it.

**Carve Only Stationary** is generally the best choice in terms of performance, and is a good match when the [**GameObject**][3] associated with the NavMesh Obstacle is controlled by physics.

### Carve when the NavMesh Obstacle has moved

To enable this mode, clear the NavMesh Obstacle component’s **Carve Only Stationary** checkbox. When this is cleared, the carved hole is updated when the obstacle has moved more than the distance set by **Carving Move Threshold**. This mode is useful for large, slowly moving obstacles (for example, a tank that is being avoided by infantry).

> [!Note]
> When using NavMesh query methods, you should take into account that there is a one-frame delay between changing a NavMesh Obstacle and the effect that change has on the NavMesh.

## Additional Resources

- [Create a NavMesh Obstacle](./CreateNavMeshObstacle.md "Guidance on creating NavMesh Obstacles.")
- [Inner Workings of the Navigation System](./NavInnerWorkings.md#two-cases-for-obstacles "Learn more about how NavMesh Obstacles are used as part of navigation.")
- [NavMesh Obstacle scripting reference](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/AI.NavMeshObstacle.html "Full description of the NavMesh Obstacle scripting API.")

[1]: ./Glossary.md#colliders "An invisible shape that is used to handle physical collisions for an object. A collider doesn’t need to be exactly the same shape as the object’s mesh - a rough approximation is often more efficient and indistinguishable in gameplay."

[2]: ./Glossary.md#collision "A collision occurs when the physics engine detects that the colliders of two GameObjects make contact or overlap, and at least one has a Rigidbody component and is in motion."

[3]: ./Glossary.md#GameObject "The fundamental object in Unity scenes, which can represent characters, props, scenery, cameras, waypoints, and more."
