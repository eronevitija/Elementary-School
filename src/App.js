import React from "react";
import StudentList from "./students/StudentList";
import TeachersList from "./teachers/TeachersList";
import Header from "./components/Header";


import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HomeScreen from "./screens/HomeScreen";

function App() {
  return (

  
    <BrowserRouter>
     <Header />
    <Routes>
      <Route path='/' element={<HomeScreen/>}/>
      <Route path='/studentList' element={<StudentList/>}/>
      <Route path='/teacherList' element={<TeachersList/>}/>

    </Routes>
    </BrowserRouter>

     
  );
}

export default App;
