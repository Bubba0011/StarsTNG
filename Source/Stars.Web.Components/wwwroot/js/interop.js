let UI = {
    zoomFactor: 1.0,
    zoomPrecision: 2,
    screenCords: {},
    svgCoords: {},
    dotNetObjects: {}
}

function retrievePosFromCorner(e) {
    let rect = UI.svg.getBoundingClientRect();
    const x = e.clientX - rect.x
    const y = e.clientY - rect.y
    let coords = {
        X: ~~x,
        Y: ~~y
    };
    return coords
}

function retrieveElementPosition(e) {
    let pt = UI.svg.createSVGPoint();
    pt.x = e.clientX;
    pt.y = e.clientY;

    // The cursor point, translated into svg coordinates
    let cursorpt = pt.matrixTransform(UI.svg.getScreenCTM().inverse());

    let coords = {
        X: ~~cursorpt.x,
        Y: ~~cursorpt.y
    };
    return coords
}

function onWheel(e) {
    let wheelDelta = (e.deltaY > 0 ? 1.25 : 0.8);
    UI.screenCoords = retrievePosFromCorner(e);
    UI.svgCoords = retrieveElementPosition(e);
    UI.zoomFactor *= wheelDelta;

    updateViewBox(wheelDelta);
    UI.dotNetObjects.galaxyView.invokeMethodAsync('ZoomCallback', UI.zoomFactor.toPrecision(UI.zoomPrecision));
}

function updateViewBox(wheelDelta) {
    vb = UI.svg.viewBox.baseVal;

    vb.width *= wheelDelta;
    vb.height *= wheelDelta;
    vb.x = (UI.svgCoords.X - UI.screenCoords.X * UI.zoomFactor);
    vb.y = (UI.svgCoords.Y - UI.screenCoords.Y * UI.zoomFactor);
}

function onMouseDown(e) {
    if (e.button != 2) {
        return;
    }

    let svg = document.querySelector("svg");
    let focus = {
        x: e.clientX,
        y: e.clientY
    }

    function onMouseMove(e) {
        svg.viewBox.baseVal.x += (focus.x - e.clientX)*UI.zoomFactor;
        svg.viewBox.baseVal.y += (focus.y - e.clientY)*UI.zoomFactor;
        focus.x = e.clientX;
        focus.y = e.clientY;
	}
    function onMouseUp() {
        svg.removeEventListener("mousemove", onMouseMove);
	}
    svg.addEventListener("mousemove", onMouseMove);
    svg.addEventListener("mouseup", onMouseUp);
}

function retrieveScreenSize(elementId) {
    let ele = document.getElementById(elementId);
    return {
        X: ele.clientWidth,
        Y: ele.clientHeight
	}
}

function resetUI() {
    UI.zoomFactor = 1.0;
}

function bindWheelEvent(svg) {
    UI.svg = svg;
    UI.svg.addEventListener("wheel", onWheel);
}

function bindCallbackMethod(obj, name) {
    UI.dotNetObjects[name] = obj;
}
