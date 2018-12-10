using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class Cutscene : Interactable {
    public TimelineAsset timeline;
    public bool pausePlayer = true;
    [HideInInspector]
    public bool played = false;
    
    private PlayableDirector director;

    void Start() {
        director = GetComponent<PlayableDirector>();
    }

    protected override void Use(Player player) {
        if(played) return;
        played = true;

        if(pausePlayer)
            GameManager.ins.paused = true;

        director.Play(timeline);
        director.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector _) {
        director.stopped -= OnTimelineFinished;
        if(pausePlayer)
            GameManager.ins.paused = false;
    }
}