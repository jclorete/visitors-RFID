const {
    read_all_room,
    read_room,
    create_room,
    update_room,
    delete_room
} = require("./room.service");

module.exports = {
    readallroom : async (req, res) => {
        try {
            await read_all_room((err, results) =>{
            if(err){
                console.log(err);
                return;
            }   
            if(!results){
                return res.json({
                    success: false,
                    message: "error"
                });
            }
            return res.json({
                success: true,
                data: results
            });
        })
        } catch (error) {
            console.log(error);
            res.status(500).send("SERVER ERROR!")
        }
  
    },
    readroom : async (req, res) => {
        try {
            const id = req.params.id
            await read_room(id,(err, results) =>{
                if(err){
                    console.log(err.error);
                    return;
                }   
                if(!results){
                    return res.json({
                        success: 0,
                        message: "error"
                    });
                }
                return res.json({
                    success: 1,
                    data: results
                });
            })
        } catch (error) {
            console.log(error);
            res.status(500).send("SERVER ERROR!")
        }
 
    },
    createroom : async (req, res) => {
        try {
            const data = req.body;
            await create_room(data,(err, results) =>{
                if(err){
                    console.log(err);
                    return;
                }   
                if(!results){
                    return res.json({
                        success: 0,
                        message: "error"
                    });
                }
                return res.json({
                    success: 1,
                    message: "ROOM SUCCESSFULLY CREATED",
                    data: results
                });
            })
        } catch (error) {
            console.log(error);
            res.status(500).send("SERVER ERROR!")
        }
   
    },
    updateroom : async (req, res) => {
        const id = req.params.id;
        const data = req.body;
        await update_room(id,data,(err, results) =>{
            if(err){
                console.log(err);
                return;
            }   
            if(!results){
                return res.json({
                    success: 0,
                    message: "error"
                });
            }
            return res.json({
                success: 1,
                message: "ROOM SUCCESSFULLY UPDATE!",
                data: results
            });
        })
    },
    deleteroom : async (req, res) => {
        const id = req.params.id
        await delete_room(id,(err, results) =>{
            if(err){
                console.log(err);
                return;
            }   
            if(!results){
                return res.json({
                    success: 0,
                    message: "error"
                });
            }
            return res.json({
                success: 1,
                message: "ROOM SUCCESSFULLY DELETED!",
                data: results
            });
        })
    },
    
}