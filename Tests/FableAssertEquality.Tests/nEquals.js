import * as R from 'ramda'

module.exports = {

    nEqual: function (objA, objB) {
		return R.equals (objA, objB)
    }
}
