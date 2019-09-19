module.exports = {

    DeepEq: function (objA, objB ) {
        const assert = require('assert');
        console.log(objA + " " + objB);
        assert.deepStrictEqual(objA, objB);
    }
}
