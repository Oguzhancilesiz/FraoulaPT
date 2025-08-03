import React from 'react';
import {
  Box,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Typography,
  Avatar,
  Divider,
} from '@mui/material';
import {
  Dashboard,
  FitnessCenter,
  People,
  Assessment,
  Settings,
  CalendarToday,
  Chat,
  Notifications,
  Person,
  Inventory,
  LocalShipping,
  Store,
  PlayArrow,
  Group,
  LocalOffer,
  Telegram,
} from '@mui/icons-material';

const drawerWidth = 280;

const menuItems = [
  { text: 'Dashboard', icon: <Dashboard />, path: '/' },
  { text: 'Market', icon: <Store />, path: '/marketplace' },
  { text: 'Video Programları', icon: <PlayArrow />, path: '/video-programs' },
  { text: 'Sporcu Grupları', icon: <Group />, path: '/sports-groups' },
  { text: 'Gelişim Paketleri', icon: <LocalOffer />, path: '/development-packages' },
  { text: 'Telegram Entegrasyonu', icon: <Telegram />, path: '/telegram-integration' },
  { text: 'Siparişler', icon: <Assessment />, path: '/admin/orders' },
  { text: 'Ürün Yönetimi', icon: <Inventory />, path: '/admin/products' },
  { text: 'Kargo Yönetimi', icon: <LocalShipping />, path: '/admin/shipping' },
  { text: 'Kargo Ücretleri', icon: <LocalShipping />, path: '/admin/shipping-rates' },
  { text: 'Müşteri Mesajları', icon: <Chat />, path: '/admin/messages' },
  { text: 'Antrenmanlar', icon: <FitnessCenter />, path: '/workouts' },
  { text: 'Kullanıcılar', icon: <People />, path: '/users' },
  { text: 'Raporlar', icon: <Assessment />, path: '/reports' },
  { text: 'Takvim', icon: <CalendarToday />, path: '/calendar' },
  { text: 'Bildirimler', icon: <Notifications />, path: '/notifications' },
  { text: 'Profil', icon: <Person />, path: '/profile' },
  { text: 'Ayarlar', icon: <Settings />, path: '/settings' },
];

const Sidebar: React.FC = () => {
  return (
    <Drawer
      variant="permanent"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        '& .MuiDrawer-paper': {
          width: drawerWidth,
          boxSizing: 'border-box',
          bgcolor: 'background.paper',
          borderRight: '1px solid rgba(0, 0, 0, 0.12)',
        },
      }}
    >
      <Box sx={{ p: 3 }}>
        <Box sx={{ display: 'flex', alignItems: 'center', mb: 3 }}>
          <FitnessCenter sx={{ fontSize: 32, color: 'primary.main', mr: 2 }} />
          <Typography variant="h5" component="div" sx={{ fontWeight: 700 }}>
            FraoulaPT
          </Typography>
        </Box>
      </Box>

      <Divider />

      <List sx={{ px: 2 }}>
        {menuItems.map((item, index) => (
          <ListItem key={index} disablePadding sx={{ mb: 1 }}>
            <ListItemButton
              sx={{
                borderRadius: 2,
                '&:hover': {
                  bgcolor: 'primary.light',
                  color: 'primary.contrastText',
                  '& .MuiListItemIcon-root': {
                    color: 'primary.contrastText',
                  },
                },
                '&.Mui-selected': {
                  bgcolor: 'primary.main',
                  color: 'primary.contrastText',
                  '& .MuiListItemIcon-root': {
                    color: 'primary.contrastText',
                  },
                  '&:hover': {
                    bgcolor: 'primary.dark',
                  },
                },
              }}
            >
              <ListItemIcon
                sx={{
                  color: 'text.secondary',
                  minWidth: 40,
                }}
              >
                {item.icon}
              </ListItemIcon>
              <ListItemText
                primary={item.text}
                primaryTypographyProps={{
                  fontWeight: 500,
                }}
              />
            </ListItemButton>
          </ListItem>
        ))}
      </List>

      <Box sx={{ mt: 'auto', p: 2 }}>
        <Divider sx={{ mb: 2 }} />
        <Box sx={{ display: 'flex', alignItems: 'center', p: 1 }}>
          <Avatar
            sx={{
              bgcolor: 'primary.main',
              mr: 2,
            }}
          >
            <Person />
          </Avatar>
          <Box>
            <Typography variant="body2" sx={{ fontWeight: 600 }}>
              Admin User
            </Typography>
            <Typography variant="caption" color="text.secondary">
              admin@fraoula.com
            </Typography>
          </Box>
        </Box>
      </Box>
    </Drawer>
  );
};

export default Sidebar; 
