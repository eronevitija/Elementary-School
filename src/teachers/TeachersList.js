import React, {useEffect, useState} from 'react'
import { teachersApi } from '../services/api';
import { Table, 
         TableBody, 
         TableCell, 
         TableContainer, 
         TableHead, 
         TableRow, 
         Button, Box,
         } from '@mui/material'
import InsertTeacher from './InsertTeacher';
import EditTeacher from './EditTeacher';
import { StyledTableCell, StyledTableRow } from '../styles/Tables.styled';


const TeacherList = () => {

    const [teacherList, setTeacherList] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [editTeacher,setEditTeacher] = useState(null);
    const [openEditDialog, setOpenEditDialog] = useState(false);

    
    useEffect(()=>{ 
        fetchTeachers();
    },[]);
 
        const fetchTeachers = async () => {
            try{
                setLoading(true);
                setError(null);
                const data = await teachersApi.getAllTeachers() ;
                console.log('API response:',data);

                if (!data) {
                  throw new Error('No data received from server');
                }

                setTeacherList(data);
            }
            catch(err){
                setError('Failed to fetch teachers');
                console.error('Error:', err);
                setTeacherList([]);
            }
            finally{
                setLoading(false);
            }
        };
    

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this teacher?')) {
            try {
                await teachersApi.deleteTeacher(id);
                fetchTeachers();
            } catch (error) {
                setError(error.message)
            }
        }
    }

    const handleEdit = (teacher) => {
        setEditTeacher({
            ...teacher,
            dateOfHire: teacher.dateOfHire?.split('T')[0],
        });
        setOpenEditDialog(true);
    };


  return (
  <Box>
    <InsertTeacher onTeacherAdded={fetchTeachers}/>
    <TableContainer>
<Table>
    <TableHead>
        <TableRow>
            <StyledTableCell>First Name</StyledTableCell>
            <StyledTableCell>Last Name</StyledTableCell>
            <StyledTableCell>Email</StyledTableCell>
            <StyledTableCell>PhoneNo</StyledTableCell>
            <StyledTableCell>Subject</StyledTableCell>
            <StyledTableCell>Address</StyledTableCell>
            <StyledTableCell>Date Of Hire</StyledTableCell>
            <StyledTableCell>Status</StyledTableCell>
        </TableRow>
    </TableHead>
    <TableBody>
        {
           teacherList.map((teachers) => (
            <StyledTableRow key={teachers.teacherID}>
                <TableCell>{teachers.firstName}</TableCell>
                <TableCell>{teachers.lastName}</TableCell>
                <TableCell>{teachers.email}</TableCell>
                <TableCell>{teachers.phoneNo}</TableCell>
                <TableCell>{teachers.subject}</TableCell>
                <TableCell>{teachers.address}</TableCell>
                <TableCell>{new Date(teachers.dateOfHire).toLocaleDateString()}</TableCell>
               
                <TableCell>{teachers.isActive ? 'Active' : 'Inactive'}</TableCell>
                <TableCell>
                    <Button
                    variant='contained'
                    color='primary'
                    size='small'
                    onClick={()=>handleEdit(teachers)}
                    sx={{mr:1}} //margin right
                    >
                        Edit
                    </Button>
                    <Button    
                    variant='contained'    
                    color='error'            
                    size='small'                    
                    onClick={()=>handleDelete(teachers.teacherID)}>
                        Delete
                    </Button>
                </TableCell>
            </StyledTableRow>
        ))
        }
    </TableBody>
</Table>
   </TableContainer>


   <EditTeacher 
    editTeacher={editTeacher}
    setEditTeacher={setEditTeacher}
    openEditDialog={openEditDialog}
    setOpenEditDialog={setOpenEditDialog}
    fetchTeachers={fetchTeachers}
   />
   
  </Box>
       
  )
}

export default TeacherList