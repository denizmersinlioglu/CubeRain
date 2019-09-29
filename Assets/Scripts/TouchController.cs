using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TouchController : MonoBehaviour {
    // Start is called before the first frame update

    CompositeDisposable disposables;
    private float TopMargin = 150;



    void Start() {
        disposables = new CompositeDisposable();
        SetupSubscriptions();
    }

    private void SetupSubscriptions() {
        Debug.Log("Touch Controller Setup Subscriptions");

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .SelectMany(_ => gameObject.UpdateAsObservable())
            .TakeUntil(this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0)))
            .Select(_ => Input.mousePosition)
            .RepeatUntilDestroy(this)
            .Subscribe(
                pos => {
                    Debug.Log(string.Format("{0} : {1}", pos.x, pos.y));

                })
            .AddTo(disposables);
    }

    // Update is called once per frame
    void Update() {

    }
}
