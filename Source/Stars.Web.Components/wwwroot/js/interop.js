let UI = {
    zoomFactor: 1.0,
    dotNetObjects: {}
}

function retrievePosFromCorner(element, event) {
    let rect = element.getBoundingClientRect();
    const x = event.clientX - rect.x
    const y = event.clientY - rect.y
    let coords = {
        X: ~~x,
        Y: ~~y
    };
    return coords
}

function retrieveElementPosition(ele, evt) {
    let pt = ele.createSVGPoint();
    pt.x = evt.clientX;
    pt.y = evt.clientY;

    // The cursor point, translated into svg coordinates
    let cursorpt = pt.matrixTransform(ele.getScreenCTM().inverse());

    let coords = {
        X: ~~cursorpt.x,
        Y: ~~cursorpt.y
    };
    return coords
}

function onWheel(evt) {
    let wheelDelta = (evt.deltaY > 0 ? 1.25 : 0.8);
    let screenCoords = retrievePosFromCorner(UI.svg, evt);
    let svgCoords = retrieveElementPosition(UI.svg, evt);
    UI.zoomFactor *= wheelDelta;

    UI.svg.viewBox.baseVal.width *= wheelDelta;
    UI.svg.viewBox.baseVal.height *= wheelDelta;
    UI.svg.viewBox.baseVal.x = (svgCoords.X - screenCoords.X * UI.zoomFactor);
    UI.svg.viewBox.baseVal.y = (svgCoords.Y - screenCoords.Y * UI.zoomFactor);
    UI.dotNetObjects.galaxyView.invokeMethodAsync('ZoomCallback', UI.zoomFactor.toPrecision(2));

    return UI.zoomFactor;
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
    svg.addEventListener("wheel", onWheel);
    UI.svg = svg;
}

function bindCallbackMethod(obj, name) {
    UI.dotNetObjects[name] = obj;
}
