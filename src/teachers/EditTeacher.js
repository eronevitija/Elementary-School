import React, { useState } from 'react'
import { teachersApi } from '../services/api'
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

const EditTeacher = ({ editTeacher, setEditTeacher, openEditDialog, setOpenEditDialog, fetchTeachers }) => {

    const [error, setError] = useState('');

 const handleEditChange = (e) => {
        const {name, value } = e.target;
        setEditTeacher(prev => ({
            ...prev,
            [name] : value
        }));
    };

    const handleEditSubmit = async () =>{
        console.log('TeacherID:', editTeacher.teacherID)
        try {
            await teachersApi.editTeacher(editTeacher.teacherID, editTeacher);
            setOpenEditDialog(false);
           await fetchTeachers();
            setError(null);
        } catch (error) {
            setError('Failed to update Teacher');
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
            <DialogTitle>Edit Teacher</DialogTitle>
            <DialogContent>
            {
                error && <Alert severity='error'>{error}</Alert>
            }
            {
                    editTeacher && (
                        <Box sx={{display:'flex', flexDirection:'column', gap:2,pt:2}}>
                            <TextField
                            name='firstName'
                            label='First Name'
                            value={editTeacher.firstName}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            />
                            <TextField
                            name='lastName'
                            label='Last Name'
                            value={editTeacher.lastName}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            />
                           
                            <TextField
                            name='email'
                            label='Email'
                            type='email'
                            value={editTeacher.email}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            InputLabelProps={{shrink:true}}
                            />
                            <TextField
                            name='phoneNo'
                            label='PhoneNo'
                            value={editTeacher.phoneNo}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            />
                            <TextField
                            name='subject'
                            label='Subject'
                            value={editTeacher.subject}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            />
                            <TextField
                            name='address'
                            label='Address'
                            value={editTeacher.address}
                            onChange={handleEditChange}
                            multiline
                            fullWidth
                            required
                            />
                         
                            <TextField
                            name='dateOfHire'
                            label='Date Of Hire'
                            type='date'
                            value={editTeacher.dateOfHire}
                            onChange={handleEditChange}
                            fullWidth
                            required
                            InputLabelProps={{shrink:true}}
                            />
                           <FormControl fullWidth>
                            <InputLabel>Status</InputLabel>
                            <Select
                            name='isActive'
                            value={editTeacher.isActive}
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

export default EditTeacher