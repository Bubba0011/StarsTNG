function retrievePosFromCorner(element, event) {
    var rect = element.getBoundingClientRect();
    const x = event.clientX - rect.x
    const y = event.clientY - rect.y
    var coords = {
        X: ~~x,
        Y: ~~y
    };
    return coords
}

function retrieveElementPosition(ele, evt) {
    var pt = ele.createSVGPoint();
    var rect = ele.getBoundingClientRect();
    pt.x = evt.clientX;
    pt.y = evt.clientY;

    // The cursor point, translated into svg coordinates
    var cursorpt = pt.matrixTransform(ele.getScreenCTM().inverse());

    var coords = {
        X: ~~cursorpt.x,
        Y: ~~cursorpt.y
    };
    return coords
}

function retrieveScreenSize() {
    let ele = document.getElementById('svg');
    return {
        Width: ele.clientWidth,
        Height: ele.clientHeight
	}
}

function hover(ele, evt) {
    var rect = ele.getBoundingClientRect();

    document.getElementById("SVGcoords").innerHTML = `SVG: X ${evt.clientX - rect.x}, Y ${evt.clientY - rect.y}`;
    var coords = retrieveElementPosition(ele, evt);
    document.getElementById("Calccoords").innerHTML = `Calc: X ${coords.X}, Y ${coords.Y}`;
}
