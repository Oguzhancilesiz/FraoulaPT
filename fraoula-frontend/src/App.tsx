import React from 'react';
import { ThemeProvider } from '@mui/material/styles';
import { CssBaseline, Box } from '@mui/material';
import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';
import { theme } from './theme';
import Dashboard from './components/Dashboard';
import Login from './components/Login';
import Sidebar from './components/Sidebar';
import Header from './components/Header';
import Marketplace from './pages/Marketplace';
import AdminOrders from './pages/AdminOrders';
import AdminProducts from './pages/AdminProducts';
import AdminShipping from './pages/AdminShipping';
import AdminShippingRates from './pages/AdminShippingRates';
import VideoPrograms from './pages/VideoPrograms';
import AdminMessages from './pages/AdminMessages';
import Home from './pages/Home';
import Contact from './pages/Contact';
import LiveChat from './components/LiveChat';
import SportsGroups from './pages/SportsGroups';
import DevelopmentPackages from './pages/DevelopmentPackages';
import TelegramIntegration from './pages/TelegramIntegration';
import './App.css';

const AppContent = () => {
  const [isAuthenticated, setIsAuthenticated] = React.useState(false);
  const [user, setUser] = React.useState<any>(null);
  const location = useLocation();

  const handleLogin = (userData: any) => {
    setUser(userData);
    setIsAuthenticated(true);
  };

  const handleLogout = () => {
    setUser(null);
    setIsAuthenticated(false);
  };

  return (
        <Box sx={{ display: 'flex', minHeight: '100vh', backgroundColor: '#F8F9FA' }}>
          {/* Sidebar sadece authenticated users için */}
          {isAuthenticated && location.pathname !== '/home' && location.pathname !== '/contact' && <Sidebar />}
          
          <Box sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}>
            {/* Header her zaman göster ama farklı stillerde */}
            <Header user={user} onLogout={handleLogout} />
            
            <Box component="main" sx={{ 
              flexGrow: 1, 
              p: location.pathname === '/home' || location.pathname === '/contact' ? 0 : 3 
            }}>
              <Routes>
                <Route 
                  path="/login" 
                  element={<Login onLogin={handleLogin} />} 
                />
                <Route 
                  path="/" 
                  element={
                    isAuthenticated ? (
                      <Dashboard user={user} />
                    ) : (
                      <Home />
                    )
                  } 
                />
                <Route path="/home" element={<Home />} />
                <Route path="/contact" element={<Contact />} />
                <Route 
                  path="/marketplace" 
                  element={
                    isAuthenticated ? (
                      <Marketplace />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/video-programs" 
                  element={
                    isAuthenticated ? (
                      <VideoPrograms />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/sports-groups" 
                  element={
                    isAuthenticated ? (
                      <SportsGroups />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/development-packages" 
                  element={
                    isAuthenticated ? (
                      <DevelopmentPackages />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/telegram-integration" 
                  element={
                    isAuthenticated ? (
                      <TelegramIntegration />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/admin/orders" 
                  element={
                    isAuthenticated ? (
                      <AdminOrders />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/admin/products" 
                  element={
                    isAuthenticated ? (
                      <AdminProducts />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/admin/shipping" 
                  element={
                    isAuthenticated ? (
                      <AdminShipping />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/admin/shipping-rates" 
                  element={
                    isAuthenticated ? (
                      <AdminShippingRates />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
                <Route 
                  path="/admin/messages" 
                  element={
                    isAuthenticated ? (
                      <AdminMessages />
                    ) : (
                      <Login onLogin={handleLogin} />
                    )
                  } 
                />
              </Routes>
            </Box>
          </Box>
          
          {/* Live Chat Widget */}
          <LiveChat />
        </Box>
  );
};

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <AppContent />
      </Router>
    </ThemeProvider>
  );
}

export default App;
