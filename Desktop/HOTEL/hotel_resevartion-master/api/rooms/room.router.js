const {
    readallroom,
    readroom,
    createroom,
    updateroom,
    deleteroom
} = require('./room.controller')


const router = require("express").Router();

router.get('/', readallroom);
router.get('/:id', readroom);
router.post('/create', createroom);
router.put('/:id', updateroom);
router.delete('/:id', deleteroom);

module.exports = router;