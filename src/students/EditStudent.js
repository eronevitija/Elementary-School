import React, { useState } from 'react'
import { studentsApi } from '../services/api';
import { 
         Box,
         Dialog,
         DialogTitle,
         Alert,
         TextField, DialogContent,
         FormControl,
         InputLabel,
         Select,
         MenuItem,
         DialogActions,
         Button
        } from '@mui/material'

const EditStudent = ({ editStudent, setEditStudent, openEditDialog,setOpenEditDialog, fetchStudents }) => {

        const [error, setError] = useState('');

        const handleEditChange = (e) => {
            const {name, value } = e.target;
            setEditStudent(prev => ({
                ...prev,
                [name] : value
            }));
        };

            const handleEditSubmit = async () =>{
                try {
                    await studentsApi.editStudent(editStudent.studentID, editStudent);
                    setOpenEditDialog(false);
                   await fetchStudents();
                    setError(null);
                } catch (error) {
                    setError('Failed to update Student');
                    console.log('Update Error',error);
                }
            };

  return (
    <Box>
        <Dialog
         open={openEditDialog}
         onClose={()=>setOpenEditDialog(false)}
         maxWidth='sm'
         fullWidth
        >
            <DialogTitle>Edit Student</DialogTitle>
            <DialogContent>
                 {
                     editStudent && (
                        <Box sx={{display:'flex', flexDirection:'column', gap:2,pt:2}}>
                            <TextField
                            name='firstName'
                            label='First Name'
                            value={editStudent.firstName}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            />
                            <TextField
                            name='fatherName'
                            label='Father Name'
                            value={editStudent.fatherName}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            />
                            <TextField
                            name='lastName'
                            label='Last Name'
                            value={editStudent.lastName}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            />
                            <FormControl fullWidth required>
                                <InputLabel>Gender</InputLabel>
                                    <Select
                                    name='gender'
                                    value={editStudent.gender}
                                    onChange={handleEditChange}
                                    label='gender'
                                    >
                                        <MenuItem value="Male">Male</MenuItem>
                                        <MenuItem value="Female">Female</MenuItem>
                                    </Select>
                            </FormControl>
                                <TextField
                                name='birthdate'
                                label='Birthdate'
                                type='date'
                                value={editStudent.birthdate}
                                onChange={handleEditChange}
                                fullWidth
                                required
                                InputLabelProps={{shrink:true}}
                                />
                                <TextField
                                 name='address'
                                label='Address'
                                value={editStudent.address}
                                onChange={handleEditChange}
                                multiline
                                fullWidth
                                required
                                />
                                <TextField
                                name='phoneNo'
                                label='PhoneNo'
                                value={editStudent.phoneNo}
                                onChange={handleEditChange}
                                fullWidth
                                required
                                />
                                <TextField
                                name='email'
                                label='Email'
                                type='email'
                                value={editStudent.email}
                                onChange={handleEditChange}
                                fullWidth
                                required
                                />
                                <TextField
                                name='enrollmentDate'
                                label='Enrollment Date'
                                type='date'
                                value={editStudent.enrollmentDate}
                                onChange={handleEditChange}
                                fullWidth
                                required
                                InputLabelProps={{shrink:true}}
                                />
                                <FormControl fullWidth>
                                    <InputLabel>Status</InputLabel>
                                    <Select
                                    name='isActive'
                                    value={editStudent.isActive}
                                    onChange={handleEditChange}
                                    label='Status'
                                    >
                                        <MenuItem value={true}>Active</MenuItem>
                                        <MenuItem value={false}>Inactive</MenuItem>
                                    </Select>
                                   </FormControl>
                                </Box>
                            )}
                
            </DialogContent>

    <DialogActions>
        <Button onClick={()=> setOpenEditDialog(false)}>Cancel</Button>
        <Button
        onClick={handleEditSubmit}
        variant='contained'
        color='primary'
        >Save Changes</Button>
    </DialogActions>
        </Dialog>

    </Box>
  )
}

export default EditStudent  