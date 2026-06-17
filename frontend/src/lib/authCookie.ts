
import Cookies from 'js-cookie'

const TOKEN_KEY = 'fintasker_access_token'

export const authCookie = {
  setToken: (token: string) => {
    // Menyimpan token selama 1 hari, amankan dengan SameSite & Secure
    Cookies.set(TOKEN_KEY, token, { 
      expires: 1, 
      secure: process.env.NODE_ENV === 'production',
      sameSite: 'strict'
    })
  },
  getToken: () => {
    return Cookies.get(TOKEN_KEY)
  },
  removeToken: () => {
    Cookies.remove(TOKEN_KEY)
  },
  isAuthenticated: () => {
    return !!Cookies.get(TOKEN_KEY)
  }
}