import React, { useState } from 'react'
import { ContactUSTitle,
         ContactCard,
         ContactButton,
         ContactInput,
         ContactTextArea
        } from '../styles/ContactUs.styled';

const ContactUs = () => {

    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Form submitted', {name, email, message})
    }

  return (

    <div>
      <ContactUSTitle>Contact Us</ContactUSTitle>
       <ContactCard>
        <p>Email: <a href='mailto:'>elementary-school@gmail.com</a></p>
        <p>Phone: 383 111 222</p>
        <form onSubmit={handleSubmit}> 
            <ContactInput
             type='text'
             placeholder='Name'
             value={name}
             onChange={(e)=>setName(e.target.value)}
            />
            <ContactInput
             type='email'
             placeholder='Email'
             value={email}
             onChange={(e)=>setEmail(e.target.value)}
            />
            <ContactTextArea
             placeholder='Message'
             value={message}
             onChange={(e)=>setMessage(e.target.value)}
            />
        <ContactButton type='submit'>Send a Message</ContactButton>
        </form>
       </ContactCard>
    </div>


  )
}

export default ContactUs