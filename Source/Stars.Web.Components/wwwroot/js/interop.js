//function retrieveElementPosition(element, event) {
//    var rect = element.getBoundingClientRect();
//    const x = event.clientX - rect.x
//    const y = event.clientY - rect.y
//    var coords = {
//        X: ~~x,
//        Y: ~~y
//    };
//    return coords
//}

function retrieveElementPosition(ele, evt) {
    var pt = ele.createSVGPoint();
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
