# Unity Inventory System

A modular inventory system developed in Unity using C# and ScriptableObjects.

The project focuses on separating item data from inventory logic, allowing different GameObjects to have their own inventory while sharing the same item definitions.

## Features

- **ScriptableObject-based items** – Create and configure item definitions directly in the Unity Editor.
- **Modular inventory architecture** – Each GameObject with an `InventoryHolder` can have its own inventory.
- **Stack management** – Supports item stacking, stack limits and stack splitting.
- **World item pickups** – Pick up items through trigger-based interactions.
- **Inventory events** – Uses Unity Events to notify other systems when inventory slots change.
- **Reusable components** – Inventory logic is separated from the GameObjects that use it.

## Technical Overview

The system is built around a few core components:

| Component | Responsibility |
|---|---|
| `InventoryItemData` | Defines item data through ScriptableObjects. |
| `InventorySlot` | Stores an item reference and its stack size. |
| `InventorySystem` | Handles inventory storage and item management. |
| `InventoryHolder` | Gives a GameObject its own inventory instance. |
| `ItemPickup` | Handles collecting items from the world. |

## Built With

- Unity
- C#
- ScriptableObjects
- Unity Events
