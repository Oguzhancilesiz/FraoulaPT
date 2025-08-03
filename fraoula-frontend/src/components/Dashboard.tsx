import React from 'react';
import {
  Box,
  Grid,
  Card,
  CardContent,
  Typography,
  Avatar,
  LinearProgress,
  Chip,
} from '@mui/material';
import {
  FitnessCenter,
  People,
  TrendingUp,
  Schedule,
  Assessment,
  Notifications,
} from '@mui/icons-material';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';

interface DashboardProps {
  user: any;
}

const Dashboard: React.FC<DashboardProps> = ({ user }) => {
  const chartData = [
    { name: 'Pzt', kalori: 420, antrenman: 45 },
    { name: 'Sal', kalori: 380, antrenman: 60 },
    { name: 'Çar', kalori: 520, antrenman: 50 },
    { name: 'Per', kalori: 610, antrenman: 75 },
    { name: 'Cum', kalori: 750, antrenman: 80 },
    { name: 'Cmt', kalori: 920, antrenman: 90 },
    { name: 'Paz', kalori: 680, antrenman: 30 },
  ];

  const stats = [
    {
      title: 'Aktif Programlar',
      value: '3',
      icon: <FitnessCenter />,
      color: '#FF6B35',
      progress: 75,
    },
    {
      title: 'Tamamlanan Antrenman',
      value: '28',
      icon: <Assessment />,
      color: '#27AE60',
      progress: 85,
    },
    {
      title: 'Bu Hafta',
      value: '5 Gün',
      icon: <Schedule />,
      color: '#F39C12',
      progress: 70,
    },
    {
      title: 'Motivasyon Puanı',
      value: '9.2/10',
      icon: <TrendingUp />,
      color: '#9B59B6',
      progress: 92,
    },
  ];

  const recentActivities = [
    { action: '💪 Bugünün antrenmanı tamamlandı!', time: '2 saat önce', type: 'success' },
    { action: '🎯 Haftalık hedef %80 gerçekleşti', time: '5 saat önce', type: 'info' },
    { action: '📹 Yeni video programına erişim açıldı', time: '1 gün önce', type: 'success' },
    { action: '🏆 30 gün motivasyon rozeti kazandın!', time: '2 gün önce', type: 'success' },
    { action: '🛒 Whey protein siparişin teslim edildi', time: '3 gün önce', type: 'info' },
  ];

  return (
    <Box>
      {/* Header */}
      <Box sx={{ mb: 4 }}>
        <Typography variant="h4" gutterBottom sx={{ color: '#FF6B35', fontWeight: 700 }}>
          🔥 Hoş geldin, {user?.name || 'Fraoula Üyesi'}!
        </Typography>
        <Typography variant="h6" color="text.secondary">
          Fitness yolculuğunda bir gün daha! Bugün kendini aş 💪
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
          {new Date().toLocaleDateString('tr-TR', { 
            weekday: 'long', 
            year: 'numeric', 
            month: 'long', 
            day: 'numeric' 
          })}
        </Typography>
      </Box>

      {/* Stats Cards */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        {stats.map((stat, index) => (
          <Grid xs={12} sm={6} md={3} key={index}>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <Avatar
                    sx={{
                      bgcolor: stat.color,
                      mr: 2,
                    }}
                  >
                    {stat.icon}
                  </Avatar>
                  <Box>
                    <Typography variant="h4" component="div">
                      {stat.value}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {stat.title}
                    </Typography>
                  </Box>
                </Box>
                <LinearProgress
                  variant="determinate"
                  value={stat.progress}
                  sx={{
                    height: 8,
                    borderRadius: 4,
                    bgcolor: 'grey.200',
                    '& .MuiLinearProgress-bar': {
                      bgcolor: stat.color,
                    },
                  }}
                />
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      {/* Charts and Activities */}
      <Grid container spacing={3}>
        {/* Chart */}
        <Grid xs={12} lg={8}>
          <Card>
            <CardContent>
              <Typography variant="h6" gutterBottom sx={{ color: '#FF6B35', fontWeight: 600 }}>
                📊 Haftalık Fitness İlerleme
              </Typography>
              <ResponsiveContainer width="100%" height={300}>
                <LineChart data={chartData}>
                  <CartesianGrid strokeDasharray="3 3" />
                  <XAxis dataKey="name" />
                  <YAxis />
                  <Tooltip />
                  <Line
                    type="monotone"
                    dataKey="kalori"
                    stroke="#FF6B35"
                    strokeWidth={3}
                    name="Yakılan Kalori"
                  />
                  <Line
                    type="monotone"
                    dataKey="antrenman"
                    stroke="#FFA500"
                    strokeWidth={3}
                    name="Antrenman Süresi (dk)"
                  />
                </LineChart>
              </ResponsiveContainer>
            </CardContent>
          </Card>
        </Grid>

        {/* Recent Activities */}
        <Grid xs={12} lg={4}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Notifications sx={{ mr: 1, color: 'primary.main' }} />
                <Typography variant="h6">Son Aktiviteler</Typography>
              </Box>
              <Box>
                {recentActivities.map((activity, index) => (
                  <Box
                    key={index}
                    sx={{
                      display: 'flex',
                      alignItems: 'center',
                      mb: 2,
                      p: 1,
                      borderRadius: 1,
                      bgcolor: 'grey.50',
                    }}
                  >
                    <Box sx={{ flexGrow: 1 }}>
                      <Typography variant="body2" sx={{ mb: 0.5 }}>
                        {activity.action}
                      </Typography>
                      <Typography variant="caption" color="text.secondary">
                        {activity.time}
                      </Typography>
                    </Box>
                    <Chip
                      label={activity.type}
                      size="small"
                      color={activity.type as any}
                      variant="outlined"
                    />
                  </Box>
                ))}
              </Box>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Quick Actions */}
      <Box sx={{ mt: 4 }}>
        <Typography variant="h6" gutterBottom>
          Hızlı İşlemler
        </Typography>
        <Grid container spacing={2}>
          <Grid xs={12} sm={6} md={3}>
            <Card sx={{ cursor: 'pointer', '&:hover': { bgcolor: 'grey.50' } }}>
              <CardContent sx={{ textAlign: 'center' }}>
                <FitnessCenter sx={{ fontSize: 40, color: 'primary.main', mb: 1 }} />
                <Typography variant="h6">Antrenman Ekle</Typography>
              </CardContent>
            </Card>
          </Grid>
          <Grid xs={12} sm={6} md={3}>
            <Card sx={{ cursor: 'pointer', '&:hover': { bgcolor: 'grey.50' } }}>
              <CardContent sx={{ textAlign: 'center' }}>
                <Assessment sx={{ fontSize: 40, color: 'primary.main', mb: 1 }} />
                <Typography variant="h6">Rapor Oluştur</Typography>
              </CardContent>
            </Card>
          </Grid>
          <Grid xs={12} sm={6} md={3}>
            <Card sx={{ cursor: 'pointer', '&:hover': { bgcolor: 'grey.50' } }}>
              <CardContent sx={{ textAlign: 'center' }}>
                <People sx={{ fontSize: 40, color: 'primary.main', mb: 1 }} />
                <Typography variant="h6">Kullanıcı Yönet</Typography>
              </CardContent>
            </Card>
          </Grid>
          <Grid xs={12} sm={6} md={3}>
            <Card sx={{ cursor: 'pointer', '&:hover': { bgcolor: 'grey.50' } }}>
              <CardContent sx={{ textAlign: 'center' }}>
                <Schedule sx={{ fontSize: 40, color: 'primary.main', mb: 1 }} />
                <Typography variant="h6">Program Planla</Typography>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      </Box>
    </Box>
  );
};

export default Dashboard; 
