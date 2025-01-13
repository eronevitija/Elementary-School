import React, { useState } from 'react';
import { Dialog, 
    DialogContent,
    DialogTitle, 
    DialogActions, 
    TextField, 
    FormControl, 
    InputLabel, 
    Box, 
    Button, 
    Select,
    MenuItem} from '@mui/material';
import { teachersApi } from '../services/api';

const InsertTeacher = ({ onTeacherAdded }) => {

    const [openDialog, setOpenDialog] = useState(false);
    const [error, setError] = useState('');

    
    const [newTeacher, setNewTeacher] = useState({
        firstName:'',
        lastName:'',
        email:'',
        phoneNo:'',
        subject:'',
        address:'',
        dateOfHire:'',
        isActive:''        
    });

    const handleEdit = (e) => {
        const {name,value} = e.target;
        setNewTeacher(prev=>({
            ...prev,
            [name]:value
        }));
    };
    
    const teacherData = {
        ...newTeacher,
        isActive:Boolean(newTeacher.isActive)
    };


const handleSubmit = async () => {
    try {
        await teachersApi.insertTeacher(teacherData);
        setOpenDialog(false);
        if (onTeacherAdded) {
            onTeacherAdded();
        }
        setError(null);

        setNewTeacher({
            firstName:'',
            lastName:'',
            email:'',
            phoneNo:'',
            subject:'',
            address:'',
            dateOfHire:'',
            isActive:'' 
        });
    } catch (error) {
        setError('Failed to add Teacher');
        console.log('Insert error',error);
    }
}


  return (
    <>
    <Box sx={{padding:2}}>
        <Button
            variant='contained'
            color='primary'
            onClick={()=> setOpenDialog(true)}
        >
            Add new Teacher
        </Button>
    </Box>

    <Dialog
        open={openDialog}
        onClose={()=>setOpenDialog(false)}
        maxWidth='sm'
        fullWidth
    >
        <DialogTitle>Add new Teacher</DialogTitle>
        <DialogContent>
            <Box sx={{ display:'flex',flexDirection:'column',gap:2,pt:2 }}>
                <TextField
                name='firstName'
                label='First Name'
                value={newTeacher.firstName}
                onChange={handleEdit}
                fullWidth
                required
                />
                <TextField
                name='lastName'
                label='Last Name'
                value={newTeacher.lastName}
                onChange={handleEdit}
                fullWidth
                required
                />
                <TextField
                name='email'
                label='Email'
                value={newTeacher.email}
                onChange={handleEdit}
                fullWidth
                required
                />
                <TextField
                name='phoneNo'
                label='PhoneNo'
                value={newTeacher.phoneNo}
                onChange={handleEdit}
                fullWidth
                required
                />
                <TextField
                name='subject'
                label='Subject'
                value={newTeacher.subject}
                onChange={handleEdit}
                fullWidth
                required
                />
                <TextField
                name='address'
                label='Address'
                value={newTeacher.address}
                onChange={handleEdit}
                fullWidth
                required
                />
                <TextField
                name='dateOfHire'
                label='Date of Hire'
                value={newTeacher.dateOfHire}
                onChange={handleEdit}
                fullWidth
                required
                />
                <FormControl fullWidth>
                    <InputLabel>Status</InputLabel>
                    <Select
                    name='isActive'
                    value={newTeacher.isActive}
                    onChange={handleEdit}
                    label='status'
                    >
                        <MenuItem value={true}>Active</MenuItem>
                        <MenuItem value={false}>Inactive</MenuItem>
                    </Select>
                </FormControl>
            </Box>
        </DialogContent>

         <DialogActions>
                    <Button onClick={handleSubmit} variant='contained' color='primary'>Add</Button>
                    <Button onClick={()=> setOpenDialog(false)}>Cancel</Button>
                </DialogActions>
    </Dialog>
    </>
  )
}

export default InsertTeacher