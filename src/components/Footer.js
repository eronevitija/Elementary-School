import React from 'react';
import { FooterStyled, SocialLinks, CopyRight } from '../styles/Footer.styled';

const Footer = () => {
  return (
    <div>
        <FooterStyled>
            <SocialLinks>
                <a href='https://facebook.com' target='_blank' rel='noopener noreferrer'>Facebook</a>
                <a href='https://instagram.com' target='_blank' rel='noopener noreferrer'>Instagram</a>

            </SocialLinks>

            <CopyRight>
                &copy; { new Date().getFullYear()} ElementarySchool. All Rights reserved.
            </CopyRight>
        </FooterStyled>

    </div>
  )
}

export default Footer