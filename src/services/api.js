import axios from "axios";


    const api = axios.create({
        baseURL: 'http://localhost:5000/api',
        headers:{
            'Content-Type' : 'application/json',
        }
    });

    export const studentsApi = {
        getAllStudents: async () => {
            try {
                const response = await api.get('/Students');
                console.log('API response',response);
                return response.data;
            } catch (error) {
                console.log('API Error',error)
                throw error;
            }
        },

        getStudentByID: async (id) => {
            try {
                const response = await api.get(`/Students/${id}`);
                return response;
            } catch (error) {
                throw error;
            }
        },

        insertStudent: async (data) => {
         try {
            const formattedData = {
                ...data,
                birthdate: new Date(data.birthdate).toISOString(),
                enrollmentDate: new Date(data.enrollmentDate).toISOString()
            };
            console.log('Sending data:' ,formattedData);

            const response = await api.post('/Students',formattedData);
            return response;
         } catch (error) {
            console.log('API error:', error.response?.data || error);
            throw error;
         }
        },

        editStudent: async (id,data) => {
            try {
                const formattedData = {
                    ...data,
                    birthdate: new Date(data.birthdate).toISOString(),
                    enrollmentDate: new Date(data.enrollmentDate).toISOString(),
                };
                console.log('Sending update data:',formattedData);

                const response = await api.put(`/Students/${id}`,formattedData);
                return response.data;
            } catch (error) {
                console.log('Error while editing Student',error);
            }
        },

        deleteStudent: async (id) => {
            try {
                const response = await api.delete(`/Students/${id}`);
                return response;
            } catch (error) {
                console.log('Student cant be deleted',error);
            }
        }
    }


    export const teachersApi ={
        getAllTeachers: async () => {
            try {
                const response = await api.get('/Teacher');
                console.log('API response',response);
                return response.data;
            } catch (error) {
                console.log('API Error',error)
                throw error;
            }
        },

        getTeacherByID: async (id) => {
            try {
                const response = await api.get(`/Teacher/${id}`);
                return response;
            } catch (error) {
                throw error;
            }
        },

        insertTeacher: async (data) => {
         try {
            const formattedData = {
                ...data,
                dateOfHire: new Date(data.dateOfHire).toISOString(),
            };
            console.log('Sending data:' ,formattedData);

            const response = await api.post('/Teacher',formattedData);
            return response;
         } catch (error) {
            console.log('API error:', error.response?.data || error);
            throw error;
         }
        },

        editTeacher: async (id,data) => {
            try {
                const formattedData = {
                    ...data,
                    dateOfHire: new Date(data.dateOfHire).toISOString(),

                };
                console.log('Sending update data:',formattedData);

                const response = await api.put(`/Teacher/${id}`,formattedData);
                return response.data;
            } catch (error) {
                console.log('Error while editing Teacher',error);
            }
        },

        deleteTeacher: async (id) => {
            try {
                const response = await api.delete(`/Teacher/${id}`);
                return response;
            } catch (error) {
                console.log('Teacher cant be deleted',error);
            }
        }
   
    }

