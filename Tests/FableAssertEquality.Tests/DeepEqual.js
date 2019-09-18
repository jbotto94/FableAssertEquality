module.exports = {

    DeepEq: function (objA, objB ) {
        const assert = require('assert');
        console.log(objA + " " + objB);
        assert.strictEqual(objA, objB);
}
