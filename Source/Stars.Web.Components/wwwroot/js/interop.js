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

let origin = {
    X: 0,
    Y: 0
};

function setOrigin(ele, evt) {
    origin = retrievePosFromCorner(ele, evt);
}

function moveViewBox(ele, evt, zoom) {
    let coords = retrievePosFromCorner(ele, evt);

    let diff = {
        X: origin.X - coords.X,
        Y: origin.Y - coords.Y
    }

    ele.viewBox.baseVal.x += diff.X/zoom;
    ele.viewBox.baseVal.y += diff.Y/zoom;
    origin = coords;
}

function retrieveScreenSize(elementId) {
    let ele = document.getElementById(elementId);
    return {
        Width: ele.clientWidth,
        Height: ele.clientHeight
	}
}

function hover(ele, evt) {
    let rect = ele.getBoundingClientRect();

    document.getElementById("SVGcoords").innerHTML = `SVG: X ${evt.clientX - rect.x}, Y ${evt.clientY - rect.y}`;
    let coords = retrieveElementPosition(ele, evt);
    document.getElementById("Calccoords").innerHTML = `Calc: X ${coords.X}, Y ${coords.Y}`;
}
