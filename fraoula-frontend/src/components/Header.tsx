import React from 'react';
import {
  AppBar,
  Toolbar,
  Typography,
  Box,
  IconButton,
  Avatar,
  Menu,
  MenuItem,
  Badge,
  Tooltip,
} from '@mui/material';
import {
  Notifications,
  AccountCircle,
  Logout,
  Settings,
  Person,
  Home,
  Store,
  ContactMail,
  PlayArrow,
} from '@mui/icons-material';
import { useNavigate, useLocation } from 'react-router-dom';

interface HeaderProps {
  user: any;
  onLogout: () => void;
}

const Header: React.FC<HeaderProps> = ({ user, onLogout }) => {
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const navigate = useNavigate();
  const location = useLocation();

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    onLogout();
    handleClose();
  };

  const publicNavItems = [
    { label: 'Ana Sayfa', path: '/home', icon: <Home /> },
    { label: 'Video Programları', path: '/video-programs', icon: <PlayArrow /> },
    { label: 'Mağaza', path: '/marketplace', icon: <Store /> },
    { label: 'İletişim', path: '/contact', icon: <ContactMail /> },
  ];

  return (
    <AppBar 
      position="static" 
      elevation={0}
      sx={{ 
        bgcolor: 'white', 
        color: '#333',
        borderBottom: '1px solid #E0E0E0'
      }}
    >
      <Toolbar>
        {/* Logo */}
        <Typography 
          variant="h5" 
          component="div" 
          sx={{ 
            fontWeight: 900,
            background: 'linear-gradient(45deg, #FF6B35, #F7931E)',
            backgroundClip: 'text',
            WebkitBackgroundClip: 'text',
            WebkitTextFillColor: 'transparent',
            mr: 4
          }}
        >
          FRAOULA
        </Typography>

        {/* Public Navigation */}
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, flexGrow: 1 }}>
          {publicNavItems.map((item) => (
            <IconButton
              key={item.path}
              onClick={() => navigate(item.path)}
              sx={{
                color: location.pathname === item.path ? '#FF6B35' : '#666',
                borderRadius: 2,
                px: 2,
                '&:hover': { 
                  bgcolor: '#FFF8F0', 
                  color: '#FF6B35' 
                }
              }}
            >
              {item.icon}
              <Typography variant="body2" sx={{ ml: 1, fontWeight: 600 }}>
                {item.label}
              </Typography>
            </IconButton>
          ))}
        </Box>

        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
          {/* Notifications */}
          <Tooltip title="Bildirimler">
            <IconButton color="inherit">
              <Badge badgeContent={4} color="error">
                <Notifications />
              </Badge>
            </IconButton>
          </Tooltip>

          {/* User Menu */}
          <Tooltip title="Kullanıcı Menüsü">
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleMenu}
              color="inherit"
            >
              <Avatar sx={{ width: 32, height: 32, bgcolor: 'primary.main' }}>
                <Person />
              </Avatar>
            </IconButton>
          </Tooltip>

          <Menu
            id="menu-appbar"
            anchorEl={anchorEl}
            anchorOrigin={{
              vertical: 'bottom',
              horizontal: 'right',
            }}
            keepMounted
            transformOrigin={{
              vertical: 'top',
              horizontal: 'right',
            }}
            open={Boolean(anchorEl)}
            onClose={handleClose}
          >
            <MenuItem onClick={handleClose}>
              <Person sx={{ mr: 1 }} />
              Profil
            </MenuItem>
            <MenuItem onClick={handleClose}>
              <Settings sx={{ mr: 1 }} />
              Ayarlar
            </MenuItem>
            <MenuItem onClick={handleLogout}>
              <Logout sx={{ mr: 1 }} />
              Çıkış Yap
            </MenuItem>
          </Menu>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Header; 
