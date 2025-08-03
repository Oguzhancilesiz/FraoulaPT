import React, { useState } from 'react';
import {
  Container,
  Grid,
  Card,
  CardContent,
  Typography,
  Button,
  Box,
  TextField,
  IconButton,
  Paper,
  List,
  ListItem,
  ListItemText,
  ListItemAvatar,
  Avatar,
  Chip,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Switch,
  FormControlLabel,
  Alert,
  LinearProgress
} from '@mui/material';
import {
  Telegram,
  Send,
  Group,
  Settings,
  Refresh,
  Add,
  Edit,
  Delete,
  CheckCircle,
  Error,
  Warning,
  People,
  Message,
  Schedule,
  TrendingUp
} from '@mui/icons-material';

const TelegramIntegration = () => {
  const [botSettings, setBotSettings] = useState({
    botToken: 'YOUR_BOT_TOKEN',
          botUsername: '@fraoula_fitness_bot',
    isActive: true,
    autoWelcome: true,
    autoAssignments: true,
    progressReminders: true
  });

  const [groups, setGroups] = useState([
    {
      id: "1",
      name: "Elite Fitness Warriors",
      telegramGroupId: "-1001234567890",
      telegramGroupLink: "https://t.me/elitefitness",
      memberCount: 32,
      isActive: true,
      lastActivity: "2 saat önce",
      botStatus: "active"
    },
    {
      id: "2",
      name: "Başlangıç Grubu",
      telegramGroupId: "-1001234567891",
      telegramGroupLink: "https://t.me/beginnerfit",
      memberCount: 28,
      isActive: true,
      lastActivity: "30 dakika önce",
      botStatus: "active"
    },
    {
      id: "3",
      name: "Elite Performance",
      telegramGroupId: "-1001234567892",
      telegramGroupLink: "https://t.me/eliteperf",
      memberCount: 18,
      isActive: true,
      lastActivity: "5 dakika önce",
      botStatus: "error"
    }
  ]);

  const [messageDialogOpen, setMessageDialogOpen] = useState(false);
  const [newMessage, setNewMessage] = useState({
    targetGroup: '',
    messageType: 'announcement',
    title: '',
    content: '',
    scheduleDate: '',
    includeButtons: false
  });

  const [botCommands] = useState([
    { command: '/start', description: 'Bot\'u başlat ve hoş geldin mesajı al' },
    { command: '/progress', description: 'İlerleme durumunu görüntüle' },
    { command: '/assignment', description: 'Günlük ödev al' },
    { command: '/schedule', description: 'Haftalık program görüntüle' },
    { command: '/support', description: 'Destek al' },
    { command: '/stats', description: 'İstatistikleri görüntüle' }
  ]);

  const [recentActivities] = useState([
          { type: 'join', user: 'Ahmet Y.', group: 'Elite Fitness', time: '5 dk önce' },
    { type: 'assignment', user: 'Zeynep K.', action: 'Ödev teslim etti', time: '15 dk önce' },
          { type: 'message', user: 'Fraoula', action: 'Grup mesajı gönderdi', time: '1 saat önce' },
    { type: 'progress', user: 'Can D.', action: 'İlerleme raporu gönderdi', time: '2 saat önce' }
  ]);

  const messageTypes = [
    { value: 'announcement', label: 'Duyuru', icon: '📢' },
    { value: 'assignment', label: 'Ödev', icon: '📝' },
    { value: 'motivation', label: 'Motivasyon', icon: '💪' },
    { value: 'reminder', label: 'Hatırlatma', icon: '⏰' },
    { value: 'celebration', label: 'Kutlama', icon: '🎉' }
  ];

  const handleSendMessage = () => {
    // Mesaj gönderme işlemi
    alert(`Mesaj "${newMessage.title}" ${groups.find(g => g.id === newMessage.targetGroup)?.name} grubuna gönderildi!`);
    setMessageDialogOpen(false);
    setNewMessage({
      targetGroup: '',
      messageType: 'announcement',
      title: '',
      content: '',
      scheduleDate: '',
      includeButtons: false
    });
  };

  const getActivityIcon = (type: string) => {
    switch (type) {
      case 'join': return <People sx={{ color: '#27AE60' }} />;
      case 'assignment': return <Schedule sx={{ color: '#FF6B35' }} />;
      case 'message': return <Message sx={{ color: '#3498DB' }} />;
      case 'progress': return <TrendingUp sx={{ color: '#9B59B6' }} />;
      default: return <Message />;
    }
  };

  const getBotStatusColor = (status: string) => {
    switch (status) {
      case 'active': return '#27AE60';
      case 'error': return '#E74C3C';
      case 'warning': return '#F39C12';
      default: return '#95A5A6';
    }
  };

  const stats = [
    {
      title: 'Toplam Üye',
      value: groups.reduce((sum, g) => sum + g.memberCount, 0),
      icon: <People />,
      color: '#FF6B35'
    },
    {
      title: 'Aktif Grup',
      value: groups.filter(g => g.isActive).length,
      icon: <Group />,
      color: '#27AE60'
    },
    {
      title: 'Günlük Mesaj',
      value: '127',
      icon: <Message />,
      color: '#3498DB'
    },
    {
      title: 'Bot Durumu',
      value: botSettings.isActive ? 'Aktif' : 'Pasif',
      icon: <Telegram />,
      color: botSettings.isActive ? '#27AE60' : '#E74C3C'
    }
  ];

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      {/* Header */}
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 4 }}>
        <Typography variant="h4" sx={{ fontWeight: 700, color: '#FF6B35' }}>
          🤖 Telegram Entegrasyonu
        </Typography>
        <Box sx={{ display: 'flex', gap: 2 }}>
          <Button
            variant="outlined"
            startIcon={<Send />}
            onClick={() => setMessageDialogOpen(true)}
            sx={{ borderColor: '#0088CC', color: '#0088CC' }}
          >
            Mesaj Gönder
          </Button>
          <Button
            variant="contained"
            startIcon={<Settings />}
            sx={{ bgcolor: '#0088CC', '&:hover': { bgcolor: '#006BB3' } }}
          >
            Bot Ayarları
          </Button>
        </Box>
      </Box>

      {/* Stats Cards */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        {stats.map((stat, index) => (
          <Grid xs={12} sm={6} md={3} key={index}>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <Avatar sx={{ bgcolor: stat.color, mr: 2 }}>
                    {stat.icon}
                  </Avatar>
                  <Box>
                    <Typography variant="h5">{stat.value}</Typography>
                    <Typography variant="body2" color="text.secondary">
                      {stat.title}
                    </Typography>
                  </Box>
                </Box>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      <Grid container spacing={4}>
        {/* Bot Settings */}
        <Grid xs={12} lg={6}>
          <Card sx={{ borderRadius: 3 }}>
            <CardContent>
              <Typography variant="h6" sx={{ mb: 3, color: '#0088CC', fontWeight: 600 }}>
                🔧 Bot Ayarları
              </Typography>

              <Box sx={{ mb: 3 }}>
                <TextField
                  fullWidth
                  label="Bot Token"
                  value={botSettings.botToken}
                  onChange={(e) => setBotSettings({...botSettings, botToken: e.target.value})}
                  type="password"
                  sx={{ mb: 2 }}
                />
                <TextField
                  fullWidth
                  label="Bot Kullanıcı Adı"
                  value={botSettings.botUsername}
                  onChange={(e) => setBotSettings({...botSettings, botUsername: e.target.value})}
                />
              </Box>

              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
                <FormControlLabel
                  control={
                    <Switch
                      checked={botSettings.isActive}
                      onChange={(e) => setBotSettings({...botSettings, isActive: e.target.checked})}
                    />
                  }
                  label="Bot Aktif"
                />
                <FormControlLabel
                  control={
                    <Switch
                      checked={botSettings.autoWelcome}
                      onChange={(e) => setBotSettings({...botSettings, autoWelcome: e.target.checked})}
                    />
                  }
                  label="Otomatik Hoş Geldin"
                />
                <FormControlLabel
                  control={
                    <Switch
                      checked={botSettings.autoAssignments}
                      onChange={(e) => setBotSettings({...botSettings, autoAssignments: e.target.checked})}
                    />
                  }
                  label="Otomatik Ödev Gönderimi"
                />
                <FormControlLabel
                  control={
                    <Switch
                      checked={botSettings.progressReminders}
                      onChange={(e) => setBotSettings({...botSettings, progressReminders: e.target.checked})}
                    />
                  }
                  label="İlerleme Hatırlatmaları"
                />
              </Box>

              <Button
                variant="contained"
                fullWidth
                sx={{ mt: 3, bgcolor: '#27AE60', '&:hover': { bgcolor: '#219A52' } }}
              >
                Ayarları Kaydet
              </Button>
            </CardContent>
          </Card>

          {/* Bot Commands */}
          <Card sx={{ borderRadius: 3, mt: 3 }}>
            <CardContent>
              <Typography variant="h6" sx={{ mb: 3, color: '#9B59B6', fontWeight: 600 }}>
                ⚡ Bot Komutları
              </Typography>

              <List>
                {botCommands.map((cmd, index) => (
                  <ListItem key={index}>
                    <ListItemText
                      primary={
                        <Typography variant="body1" sx={{ fontFamily: 'monospace', color: '#0088CC' }}>
                          {cmd.command}
                        </Typography>
                      }
                      secondary={cmd.description}
                    />
                  </ListItem>
                ))}
              </List>
            </CardContent>
          </Card>
        </Grid>

        {/* Telegram Groups */}
        <Grid xs={12} lg={6}>
          <Card sx={{ borderRadius: 3 }}>
            <CardContent>
              <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
                <Typography variant="h6" sx={{ color: '#0088CC', fontWeight: 600 }}>
                  📱 Telegram Grupları
                </Typography>
                <IconButton sx={{ color: '#0088CC' }}>
                  <Refresh />
                </IconButton>
              </Box>

              <List>
                {groups.map((group) => (
                  <ListItem key={group.id} sx={{ 
                    border: '1px solid #E0E0E0', 
                    borderRadius: 2, 
                    mb: 2,
                    '&:hover': { bgcolor: '#F8F9FA' }
                  }}>
                    <ListItemAvatar>
                      <Avatar sx={{ bgcolor: '#0088CC' }}>
                        <Group />
                      </Avatar>
                    </ListItemAvatar>
                    <ListItemText
                      primary={
                        <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
                          <Typography variant="subtitle1" sx={{ fontWeight: 600 }}>
                            {group.name}
                          </Typography>
                          <Chip
                            size="small"
                            icon={<CheckCircle />}
                            label="Aktif"
                            sx={{
                              bgcolor: getBotStatusColor(group.botStatus),
                              color: 'white'
                            }}
                          />
                        </Box>
                      }
                      secondary={
                        <Box>
                          <Typography variant="caption" display="block">
                            👥 {group.memberCount} üye • Son aktivite: {group.lastActivity}
                          </Typography>
                          <Typography variant="caption" sx={{ color: '#0088CC' }}>
                            {group.telegramGroupLink}
                          </Typography>
                        </Box>
                      }
                    />
                  </ListItem>
                ))}
              </List>

              <Button
                variant="outlined"
                fullWidth
                startIcon={<Add />}
                sx={{ borderColor: '#0088CC', color: '#0088CC' }}
              >
                Yeni Grup Bağla
              </Button>
            </CardContent>
          </Card>

          {/* Recent Activities */}
          <Card sx={{ borderRadius: 3, mt: 3 }}>
            <CardContent>
              <Typography variant="h6" sx={{ mb: 3, color: '#F39C12', fontWeight: 600 }}>
                📊 Son Aktiviteler
              </Typography>

              <List>
                {recentActivities.map((activity, index) => (
                  <ListItem key={index}>
                    <ListItemAvatar>
                      <Avatar sx={{ bgcolor: '#F8F9FA' }}>
                        {getActivityIcon(activity.type)}
                      </Avatar>
                    </ListItemAvatar>
                    <ListItemText
                      primary={`${activity.user} ${activity.action || activity.group}`}
                      secondary={activity.time}
                    />
                  </ListItem>
                ))}
              </List>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Send Message Dialog */}
      <Dialog open={messageDialogOpen} onClose={() => setMessageDialogOpen(false)} maxWidth="md" fullWidth>
        <DialogTitle sx={{ bgcolor: '#0088CC', color: 'white' }}>
          📤 Telegram Mesajı Gönder
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          <Grid container spacing={3} sx={{ mt: 1 }}>
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Hedef Grup</InputLabel>
                <Select
                  value={newMessage.targetGroup}
                  onChange={(e) => setNewMessage({...newMessage, targetGroup: e.target.value})}
                  label="Hedef Grup"
                >
                  {groups.map((group) => (
                    <MenuItem key={group.id} value={group.id}>
                      {group.name} ({group.memberCount} üye)
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>

            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Mesaj Türü</InputLabel>
                <Select
                  value={newMessage.messageType}
                  onChange={(e) => setNewMessage({...newMessage, messageType: e.target.value})}
                  label="Mesaj Türü"
                >
                  {messageTypes.map((type) => (
                    <MenuItem key={type.value} value={type.value}>
                      {type.icon} {type.label}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>

            <Grid xs={12}>
              <TextField
                fullWidth
                label="Mesaj Başlığı"
                value={newMessage.title}
                onChange={(e) => setNewMessage({...newMessage, title: e.target.value})}
              />
            </Grid>

            <Grid xs={12}>
              <TextField
                fullWidth
                label="Mesaj İçeriği"
                multiline
                rows={4}
                value={newMessage.content}
                onChange={(e) => setNewMessage({...newMessage, content: e.target.value})}
                placeholder="Grup üyelerine göndermek istediğiniz mesajı yazın..."
              />
            </Grid>

            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Zamanlama (opsiyonel)"
                type="datetime-local"
                value={newMessage.scheduleDate}
                onChange={(e) => setNewMessage({...newMessage, scheduleDate: e.target.value})}
                InputLabelProps={{ shrink: true }}
              />
            </Grid>

            <Grid xs={12} md={6}>
              <FormControlLabel
                control={
                  <Switch
                    checked={newMessage.includeButtons}
                    onChange={(e) => setNewMessage({...newMessage, includeButtons: e.target.checked})}
                  />
                }
                label="Etkileşim Butonları Ekle"
              />
            </Grid>
          </Grid>

          {newMessage.targetGroup && (
            <Alert severity="info" sx={{ mt: 2 }}>
              Mesaj <strong>{groups.find(g => g.id === newMessage.targetGroup)?.name}</strong> grubundaki{' '}
              <strong>{groups.find(g => g.id === newMessage.targetGroup)?.memberCount}</strong> üyeye gönderilecek.
            </Alert>
          )}
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setMessageDialogOpen(false)}>İptal</Button>
          <Button 
            variant="contained"
            onClick={handleSendMessage}
            disabled={!newMessage.targetGroup || !newMessage.content}
            sx={{ bgcolor: '#0088CC', '&:hover': { bgcolor: '#006BB3' } }}
          >
            {newMessage.scheduleDate ? 'Zamanla' : 'Hemen Gönder'}
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default TelegramIntegration;
