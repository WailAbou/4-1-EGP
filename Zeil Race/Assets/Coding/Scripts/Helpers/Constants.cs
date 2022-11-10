using UnityEngine;

public static class Constants
{
    [Header("Spawn Delays")]
    public const float GRIDPIECE_SPAWN_DELAY = 0.05f;

    [Header("Spawn Durations")]
    public const float GRIDPIECE_SPAWN_DURATION = 0.5f;
    public const float BOARD_SPAWN_DURATION = 1.5f;
    public const float PLAYER_SPAWN_DURATION = 1;
    public const float QUIZ_SPAWN_DURATION = 1;
    public const float TOASTR_SPAWN_DURATION = 1;
    public const float DICE_SPAWN_DURATION = 0.5f;

    [Header("Move Durations")]
    public const float PLAYER_MOVE_DURATION = 1;
    public const float ARROW_MOVE_DURATION = 1;
    public const float TOASTR_MOVE_DURATION = 1;
    public const float DICE_MOVE_DURATION = 1;

    [Header("End Durations")]
    public const float QUIZ_END_DURATION = 1;
    public const float TOASTR_END_DURATION = 1.5f;
    public const float DICE_END_DURATION = 1;
}
