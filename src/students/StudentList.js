import React, {useEffect, useState} from 'react'
import { studentsApi } from '../services/api';
import { 
         Table, 
         TableBody, 
         TableCell, 
         TableContainer, 
         TableHead, 
         TableRow, 
         Button, Box } from '@mui/material'
import InsertStudents from './InsertStudents';
import { StyledTableCell, StyledTableRow } from '../styles/Tables.styled';
import EditStudent from './EditStudent';

const StudentList = () => {
    const [studentList, setStudentList] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [editStudent,setEditStudent] = useState(null);
    const [openEditDialog, setOpenEditDialog] = useState(false);

    
    useEffect(()=>{ 
        fetchStudents();
    },[]);
 
        const fetchStudents = async () => {
            try{
                setLoading(true);
                const data = await studentsApi.getAllStudents() ;
                console.log('API response:',data);

                setStudentList(data);
            }
            catch(err){
                setError('Failed to fetch students');
                console.error('Error:', err);
                setStudentList([]);
            }
            finally{
                setLoading(false);
            }
        };
    

    const handleDelete = async (id) => {
        if (window.confirm('Are you sure you want to delete this student?')) {
            try {
                await studentsApi.deleteStudent(id);
                fetchStudents();
            } catch (error) {
                setError(error.message)
            }
        }
    }

    const handleEdit = (student) => {
        setEditStudent({
            ...student,
            birthdate: student.birthdate?.split('T')[0],
            enrollmentDate: student.enrollmentDate?.split('T')[0]
        });
        setOpenEditDialog(true);
    };


  return (
  <Box>
    <InsertStudents onStudentAdded={fetchStudents}/>
    <TableContainer>
    <Table>
    <TableHead>
        <TableRow>
            <StyledTableCell>First Name</StyledTableCell>
            <StyledTableCell>Father Name</StyledTableCell>
            <StyledTableCell>Last Name</StyledTableCell>
            <StyledTableCell>Gender</StyledTableCell>
            <StyledTableCell>Birthdate</StyledTableCell>
            <StyledTableCell>Address</StyledTableCell>
            <StyledTableCell>PhoneNo</StyledTableCell>
            <StyledTableCell>Email</StyledTableCell>
            <StyledTableCell>Enrollment Date</StyledTableCell>
            <StyledTableCell>Status</StyledTableCell>
        </TableRow>
    </TableHead>
    <TableBody>
        {
           studentList.map((students) => (
            <StyledTableRow key={students.studentID}>
                <TableCell>{students.firstName}</TableCell>
                <TableCell>{students.fatherName}</TableCell>
                <TableCell>{students.lastName}</TableCell>
                <TableCell>{students.gender}</TableCell>
                <TableCell>{new Date(students.birthdate).toLocaleDateString()}</TableCell>
                <TableCell>{students.address}</TableCell>
                <TableCell>{students.phoneNo}</TableCell>
                <TableCell>{students.email}</TableCell>
                <TableCell>{new Date(students.enrollmentDate).toLocaleDateString()}</TableCell>
                <TableCell>{students.isActive ? 'Active' : 'Inactive'}</TableCell>
                <TableCell>
                    <Button
                    variant='contained'
                    color='primary'
                    size='small'
                    onClick={()=>handleEdit(students)}
                    sx={{mr:1}} //margin right
                    >
                        Edit
                    </Button>
                    <Button    
                    variant='contained'    
                    color='error'            
                    size='small'                    
                    onClick={()=>handleDelete(students.studentID)}>
                        Delete
                    </Button>
                </TableCell>
            </StyledTableRow>
        ))
        }
    </TableBody>
</Table>
   </TableContainer>

   <EditStudent 
   editStudent={editStudent}
   setEditStudent={setEditStudent}
   openEditDialog={openEditDialog}
   setOpenEditDialog={setOpenEditDialog}
   fetchStudents={fetchStudents}
   />
    
  </Box>
       
  )
}

export default StudentList