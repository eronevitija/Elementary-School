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
        MenuItem,} from '@mui/material';
import { studentsApi } from '../services/api';


const InsertStudents = ({ onStudentAdded }) => {

const [openDialog, setOpenDialog] = useState(false);
const [error, setError] = useState('');
const [edit, setEdit] = useState('');

const [newStudent, setNewStudent] = useState({
    firstName:'',
    fatherName:'',
    lastName:'',
    gender:'',
    birthdate:'',
    address:'',
    phoneNo:'',
    email:'',
    enrollmentDate:'',
    isActive:true
});

const handleEdit = (e) => {
    const {name,value} = e.target;
    setNewStudent(prev=>({
        ...prev,
        [name]:value
    }));
};

const studentData = {
    ...newStudent,
    isActive:Boolean(newStudent.isActive)
};

const handleSubmit = async () => {
    try {
        await studentsApi.insertStudent(studentData);
        setOpenDialog(false);
        if (onStudentAdded) {
            onStudentAdded();
        }
        setError(null);

        setNewStudent({
            firstName:'',
            fatherName:'',
            lastName:'',
            gender:'',
            birthdate:'',
            address:'',
            phoneNo:'',
            email:'',
            enrollmentDate:'',
            isActive:true
        });
    } catch (error) {
        setError('Failed to add Student');
        console.log('Insert error',error);
    }
}



  return (
    <>
    <Box sx={{ padding:2}} >
    <Button
    variant='contained'
    color='primary'
    onClick={()=> setOpenDialog(true)}
    >
        Add new Student </Button>
    </Box>

    <Dialog
    open={openDialog}
    onClose={()=>setOpenDialog(false)}
    maxWidth='sm'
    fullWidth
    >
        <DialogTitle>Add new Student</DialogTitle>
        <DialogContent>
            <Box sx={{display:'flex',flexDirection:'column',gap:2,pt:2}}>
                <TextField
                name='firstName'
                label='First Name'
                value={newStudent.firstName}
                onChange={handleEdit}
                fullWidth
                required
                />
                <TextField
                  name='fatherName'
                  label='Father Name'
                  value={newStudent.fatherName}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <TextField
                  name='lastName'
                  label='Last Name'
                  value={newStudent.lastName}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <TextField
                  name='gender'
                  label='Gender'
                  value={newStudent.gender}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <TextField
                  name='birthdate'
                  label='Birthdate'
                  value={newStudent.birthdate}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <TextField
                  name='address'
                  label='Address'
                  value={newStudent.address}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <TextField
                  name='phoneNo'
                  label='PhoneNo'
                  value={newStudent.phoneNo}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <TextField
                  name='email'
                  label='Email'
                  value={newStudent.email}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <TextField
                  name='enrollmentDate'
                  label='Enrollment Date'
                  value={newStudent.enrollmentDate}
                  onChange={handleEdit}
                  fullWidth
                  required
                />
                <FormControl fullWidth>
                    <InputLabel>Status</InputLabel>
                    <Select
                    name='isActive'
                    value={newStudent.isActive}
                    onChange={handleEdit}
                    label='status'
                    >
                        <MenuItem value={true}>Active</MenuItem>
                        <MenuItem value={false}>Inactive</MenuItem>
                    </Select>
                </FormControl>
            </Box>
        </DialogContent>

        <DialogActions >
            <Button onClick={handleSubmit} variant='contained' color='primary'>Add</Button>
            <Button onClick={()=> setOpenDialog(false)}>Cancel</Button>
        </DialogActions>
    </Dialog>
    </>
  )
}

export default InsertStudents