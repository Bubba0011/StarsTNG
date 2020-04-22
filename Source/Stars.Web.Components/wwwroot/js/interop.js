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

function onMouseDown(e, zoom) {
    console.log(zoom)
    if (e.button != 2) {
        return;
    }

    let svg = document.querySelector("svg");
    let focus = {
        x: e.clientX,
        y: e.clientY
    }
    let zoomScale = zoom;

    function onMouseMove(e) {
        svg.viewBox.baseVal.x += (focus.x - e.clientX)/zoomScale;
        svg.viewBox.baseVal.y += (focus.y - e.clientY)/zoomScale;
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
        Width: ele.clientWidth,
        Height: ele.clientHeight
	}
}
