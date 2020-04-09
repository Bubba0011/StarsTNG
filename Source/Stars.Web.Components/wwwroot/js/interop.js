function retrieveElementPosition(element, event) {
    var rect = element.getBoundingClientRect();
    const x = event.clientX - rect.x
    const y = event.clientY - rect.y
    var coords = {
        X: ~~x,
        Y: ~~y
    };
    return coords
}