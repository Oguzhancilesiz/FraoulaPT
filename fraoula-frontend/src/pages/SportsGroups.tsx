import React, { useState } from 'react';
import {
  Container,
  Grid,
  Card,
  CardContent,
  CardMedia,
  Typography,
  Button,
  Box,
  Chip,
  Avatar,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  List,
  ListItem,
  ListItemAvatar,
  ListItemText,
  ListItemSecondaryAction,
  Paper,
  Badge,
  Divider,
  LinearProgress
} from '@mui/material';
import {
  Group,
  Add,
  TwoWheeler,
  FitnessCenter,
  People,
  Telegram,
  Assignment,
  TrendingUp,
  Schedule,
  Star,
  Edit,
  Delete,
  PersonAdd,
  Chat,
  Assessment,
  EmojiEvents,
  LocalOffer
} from '@mui/icons-material';

const SportsGroups = () => {
  const [groups, setGroups] = useState([
    {
      id: "1",
      name: "Elite Fitness Warriors",
      description: "Sporcular için özel tasarlanmış fitness grubu. Güç, dayanıklılık ve koordinasyon odaklı antrenmanlar.",
      type: "HybridGroup",
      targetLevel: "Intermediate",
      imageUrl: "/images/groups/elite-fitness.jpg",
      maxMembers: 50,
      currentMemberCount: 32,
      isActive: true,
      isPrivate: false,
      telegramGroupLink: "https://t.me/elitefitness",
      coach: "Fraoula",
      primaryFocus: "Strength",
      startDate: "2024-01-15",
      weeklySchedule: "Pzt-Çar-Cum 19:00",
      rating: 4.8,
      packages: 3,
      assignments: 12
    },
    {
      id: "2", 
      name: "Başlangıç Grubu",
      description: "Fitness dünyasına yeni adım atanlar için destekleyici topluluk.",
      type: "BeginnerGroup",
      targetLevel: "Beginner",
      imageUrl: "/images/groups/beginner.jpg",
      maxMembers: 30,
      currentMemberCount: 28,
      isActive: true,
      isPrivate: false,
      telegramGroupLink: "https://t.me/beginnerfit",
      coach: "Fraoula",
      primaryFocus: "GeneralFitness",
      startDate: "2024-02-01",
      weeklySchedule: "Sal-Per-Cmt 18:00",
      rating: 4.9,
      packages: 2,
      assignments: 8
    },
    {
      id: "3",
      name: "Elite Performance",
      description: "İleri seviye sporcular için yoğun antrenman ve rekabet ortamı.",
      type: "AdvancedGroup",
      targetLevel: "Advanced",
      imageUrl: "/images/groups/elite.jpg",
      maxMembers: 20,
      currentMemberCount: 18,
      isActive: true,
      isPrivate: true,
      telegramGroupLink: "https://t.me/eliteperf",
      coach: "Fraoula",
      primaryFocus: "Strength",
      startDate: "2024-01-01",
      weeklySchedule: "Her gün 17:00",
      rating: 5.0,
      packages: 5,
      assignments: 20
    }
  ]);

  const [selectedGroup, setSelectedGroup] = useState<any>(null);
  const [groupDialogOpen, setGroupDialogOpen] = useState(false);
  const [memberDialogOpen, setMemberDialogOpen] = useState(false);

  const [newGroupData, setNewGroupData] = useState({
    name: '',
    description: '',
    type: 'FitnessGroup',
    targetLevel: 'Beginner',
    maxMembers: 30,
    isPrivate: false,
    primaryFocus: 'GeneralFitness',
    weeklySchedule: ''
  });

  const groupTypes = [
    { value: 'FitnessGroup', label: 'Fitness Grubu', icon: <FitnessCenter />, color: '#FF6B35' },
    { value: 'FitnessClub', label: 'Fitness Kulübü', icon: <FitnessCenter />, color: '#9B59B6' },
    { value: 'HybridGroup', label: 'Hibrit Grup', icon: <Group />, color: '#E74C3C' },
    { value: 'BeginnerGroup', label: 'Başlangıç', icon: <Star />, color: '#27AE60' },
    { value: 'AdvancedGroup', label: 'İleri Seviye', icon: <TrendingUp />, color: '#F39C12' }
  ];

  const focusAreas = [
    { value: 'WeightLoss', label: 'Kilo Verme' },
    { value: 'MuscleGain', label: 'Kas Kazanma' },
    { value: 'Endurance', label: 'Dayanıklılık' },
    { value: 'Strength', label: 'Güç' },
    { value: 'Flexibility', label: 'Esneklik' },
    { value: 'Coordination', label: 'Koordinasyon' },
    { value: 'GeneralFitness', label: 'Genel Fitness' }
  ];

  const getTypeInfo = (type: string) => {
    return groupTypes.find(t => t.value === type) || groupTypes[0];
  };

  const handleCreateGroup = () => {
    const newGroup = {
      id: (Math.max(...groups.map(g => parseInt(g.id))) + 1).toString(),
      ...newGroupData,
      imageUrl: "/images/groups/default.jpg",
      currentMemberCount: 0,
      isActive: true,
      telegramGroupLink: "",
      coach: "Fraoula",
      startDate: new Date().toISOString().split('T')[0],
      rating: 5.0,
      packages: 0,
      assignments: 0
    };

    setGroups([...groups, newGroup]);
    setGroupDialogOpen(false);
    setNewGroupData({
      name: '',
      description: '',
      type: 'FitnessGroup',
      targetLevel: 'Beginner',
      maxMembers: 30,
      isPrivate: false,
      primaryFocus: 'GeneralFitness',
      weeklySchedule: ''
    });
  };

  const mockMembers = [
    { id: "1", name: "Ahmet Yılmaz", avatar: "/avatars/ahmet.jpg", role: "Member", joinDate: "2024-01-20", telegramUsername: "@ahmet_y" },
    { id: "2", name: "Zeynep Kaya", avatar: "/avatars/zeynep.jpg", role: "Moderator", joinDate: "2024-01-18", telegramUsername: "@zeynep_k" },
    { id: "3", name: "Can Demir", avatar: "/avatars/can.jpg", role: "Member", joinDate: "2024-01-25", telegramUsername: "@can_d" }
  ];

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      {/* Header */}
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 4 }}>
        <Typography variant="h4" sx={{ fontWeight: 700, color: '#FF6B35' }}>
          👥 Sporcu Grupları
        </Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setGroupDialogOpen(true)}
          sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
        >
          Yeni Grup Oluştur
        </Button>
      </Box>

      {/* Stats Cards */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Avatar sx={{ bgcolor: '#FF6B35', mr: 2 }}>
                  <Group />
                </Avatar>
                <Box>
                  <Typography variant="h5">{groups.length}</Typography>
                  <Typography variant="body2" color="text.secondary">
                    Toplam Grup
                  </Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Avatar sx={{ bgcolor: '#27AE60', mr: 2 }}>
                  <People />
                </Avatar>
                <Box>
                  <Typography variant="h5">
                    {groups.reduce((sum, g) => sum + g.currentMemberCount, 0)}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Toplam Üye
                  </Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Avatar sx={{ bgcolor: '#F39C12', mr: 2 }}>
                  <LocalOffer />
                </Avatar>
                <Box>
                  <Typography variant="h5">
                    {groups.reduce((sum, g) => sum + g.packages, 0)}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Aktif Paket
                  </Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
        <Grid xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center' }}>
                <Avatar sx={{ bgcolor: '#9B59B6', mr: 2 }}>
                  <Assignment />
                </Avatar>
                <Box>
                  <Typography variant="h5">
                    {groups.reduce((sum, g) => sum + g.assignments, 0)}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Toplam Ödev
                  </Typography>
                </Box>
              </Box>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Groups Grid */}
      <Grid container spacing={4}>
        {groups.map((group) => {
          const typeInfo = getTypeInfo(group.type);
          const occupancyRate = (group.currentMemberCount / group.maxMembers) * 100;
          
          return (
            <Grid xs={12} md={6} lg={4} key={group.id}>
              <Card sx={{
                borderRadius: 3,
                overflow: 'hidden',
                boxShadow: '0 8px 32px rgba(255, 107, 53, 0.1)',
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
                position: 'relative',
                '&:hover': {
                  transform: 'translateY(-8px)',
                  boxShadow: '0 16px 48px rgba(255, 107, 53, 0.2)'
                }
              }}>
                {/* Group Image */}
                <Box sx={{ position: 'relative' }}>
                  <CardMedia
                    component="img"
                    height="180"
                    image={group.imageUrl}
                    alt={group.name}
                  />
                  
                  {/* Type Badge */}
                  <Chip
                    icon={typeInfo.icon}
                    label={typeInfo.label}
                    size="small"
                    sx={{
                      position: 'absolute',
                      top: 12,
                      left: 12,
                      bgcolor: typeInfo.color,
                      color: 'white'
                    }}
                  />

                  {/* Privacy Badge */}
                  {group.isPrivate && (
                    <Chip
                      label="Özel"
                      size="small"
                      sx={{
                        position: 'absolute',
                        top: 12,
                        right: 12,
                        bgcolor: '#E74C3C',
                        color: 'white'
                      }}
                    />
                  )}

                  {/* Rating */}
                  <Box sx={{
                    position: 'absolute',
                    bottom: 12,
                    right: 12,
                    bgcolor: 'rgba(0,0,0,0.7)',
                    color: 'white',
                    borderRadius: 2,
                    px: 1.5,
                    py: 0.5,
                    display: 'flex',
                    alignItems: 'center'
                  }}>
                    <Star sx={{ fontSize: 16, mr: 0.5, color: '#FFD700' }} />
                    <Typography variant="caption" sx={{ fontWeight: 600 }}>
                      {group.rating}
                    </Typography>
                  </Box>
                </Box>

                <CardContent>
                  {/* Group Name & Coach */}
                  <Typography variant="h6" sx={{ fontWeight: 600, mb: 1 }}>
                    {group.name}
                  </Typography>
                  
                  <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                    <Avatar sx={{ width: 24, height: 24, mr: 1, bgcolor: '#FF6B35' }}>
                      F
                    </Avatar>
                    <Typography variant="body2" color="text.secondary">
                      Antrenör: {group.coach}
                    </Typography>
                  </Box>

                  {/* Description */}
                  <Typography variant="body2" color="text.secondary" sx={{ mb: 2, height: 40, overflow: 'hidden' }}>
                    {group.description}
                  </Typography>

                  {/* Member Count & Progress */}
                  <Box sx={{ mb: 2 }}>
                    <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 1 }}>
                      <Typography variant="body2" sx={{ fontWeight: 600 }}>
                        Üye Sayısı
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        {group.currentMemberCount}/{group.maxMembers}
                      </Typography>
                    </Box>
                    <LinearProgress
                      variant="determinate"
                      value={occupancyRate}
                      sx={{
                        height: 8,
                        borderRadius: 4,
                        bgcolor: '#F5F5F5',
                        '& .MuiLinearProgress-bar': {
                          bgcolor: occupancyRate > 80 ? '#E74C3C' : occupancyRate > 60 ? '#F39C12' : '#27AE60'
                        }
                      }}
                    />
                  </Box>

                  {/* Group Info */}
                  <Box sx={{ display: 'flex', gap: 1, mb: 2, flexWrap: 'wrap' }}>
                    <Chip label={group.targetLevel} size="small" variant="outlined" />
                    <Chip label={focusAreas.find(f => f.value === group.primaryFocus)?.label} size="small" variant="outlined" />
                  </Box>

                  {/* Schedule */}
                  <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                    <Schedule sx={{ fontSize: 16, color: '#FF6B35', mr: 1 }} />
                    <Typography variant="caption">
                      {group.weeklySchedule}
                    </Typography>
                  </Box>

                  {/* Stats */}
                  <Box sx={{ display: 'flex', gap: 2, mb: 3 }}>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <LocalOffer sx={{ fontSize: 16, color: '#FF6B35', mr: 0.5 }} />
                      <Typography variant="caption">{group.packages} Paket</Typography>
                    </Box>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <Assignment sx={{ fontSize: 16, color: '#FF6B35', mr: 0.5 }} />
                      <Typography variant="caption">{group.assignments} Ödev</Typography>
                    </Box>
                  </Box>

                  {/* Actions */}
                  <Box sx={{ display: 'flex', gap: 1 }}>
                    <Button
                      variant="contained"
                      size="small"
                      startIcon={<People />}
                      onClick={() => {
                        setSelectedGroup(group);
                        setMemberDialogOpen(true);
                      }}
                      sx={{ 
                        bgcolor: '#FF6B35', 
                        '&:hover': { bgcolor: '#E55A2B' },
                        flex: 1
                      }}
                    >
                      Üyeler
                    </Button>
                    
                    <IconButton
                      size="small"
                      sx={{ color: '#FF6B35' }}
                      onClick={() => window.open(group.telegramGroupLink, '_blank')}
                    >
                      <Telegram />
                    </IconButton>
                    
                    <IconButton
                      size="small"
                      sx={{ color: '#27AE60' }}
                    >
                      <Edit />
                    </IconButton>
                  </Box>
                </CardContent>
              </Card>
            </Grid>
          );
        })}
      </Grid>

      {/* Create Group Dialog */}
      <Dialog open={groupDialogOpen} onClose={() => setGroupDialogOpen(false)} maxWidth="md" fullWidth>
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          Yeni Sporcu Grubu Oluştur
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          <Grid container spacing={3} sx={{ mt: 1 }}>
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Grup Adı"
                value={newGroupData.name}
                onChange={(e) => setNewGroupData({...newGroupData, name: e.target.value})}
                required
              />
            </Grid>
            
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Açıklama"
                multiline
                rows={3}
                value={newGroupData.description}
                onChange={(e) => setNewGroupData({...newGroupData, description: e.target.value})}
              />
            </Grid>
            
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Grup Türü</InputLabel>
                <Select
                  value={newGroupData.type}
                  onChange={(e) => setNewGroupData({...newGroupData, type: e.target.value})}
                  label="Grup Türü"
                >
                  {groupTypes.map((type) => (
                    <MenuItem key={type.value} value={type.value}>
                      <Box sx={{ display: 'flex', alignItems: 'center' }}>
                        {type.icon}
                        <Typography sx={{ ml: 1 }}>{type.label}</Typography>
                      </Box>
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Hedef Seviye</InputLabel>
                <Select
                  value={newGroupData.targetLevel}
                  onChange={(e) => setNewGroupData({...newGroupData, targetLevel: e.target.value})}
                  label="Hedef Seviye"
                >
                  <MenuItem value="Beginner">Başlangıç</MenuItem>
                  <MenuItem value="Intermediate">Orta</MenuItem>
                  <MenuItem value="Advanced">İleri</MenuItem>
                </Select>
              </FormControl>
            </Grid>
            
            <Grid xs={12} md={6}>
              <TextField
                fullWidth
                label="Maksimum Üye Sayısı"
                type="number"
                value={newGroupData.maxMembers}
                onChange={(e) => setNewGroupData({...newGroupData, maxMembers: parseInt(e.target.value)})}
              />
            </Grid>
            
            <Grid xs={12} md={6}>
              <FormControl fullWidth>
                <InputLabel>Ana Odak</InputLabel>
                <Select
                  value={newGroupData.primaryFocus}
                  onChange={(e) => setNewGroupData({...newGroupData, primaryFocus: e.target.value})}
                  label="Ana Odak"
                >
                  {focusAreas.map((focus) => (
                    <MenuItem key={focus.value} value={focus.value}>
                      {focus.label}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            
            <Grid xs={12}>
              <TextField
                fullWidth
                label="Haftalık Program"
                placeholder="Örn: Pzt-Çar-Cum 19:00"
                value={newGroupData.weeklySchedule}
                onChange={(e) => setNewGroupData({...newGroupData, weeklySchedule: e.target.value})}
              />
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button onClick={() => setGroupDialogOpen(false)}>İptal</Button>
          <Button 
            variant="contained"
            onClick={handleCreateGroup}
            sx={{ bgcolor: '#FF6B35', '&:hover': { bgcolor: '#E55A2B' } }}
          >
            Grubu Oluştur
          </Button>
        </DialogActions>
      </Dialog>

      {/* Members Dialog */}
      <Dialog open={memberDialogOpen} onClose={() => setMemberDialogOpen(false)} maxWidth="md" fullWidth>
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          {selectedGroup?.name} - Üye Listesi
        </DialogTitle>
        <DialogContent sx={{ p: 0 }}>
          <List>
            {mockMembers.map((member, index) => (
              <ListItem key={member.id}>
                <ListItemAvatar>
                  <Avatar src={member.avatar} alt={member.name}>
                    {member.name.charAt(0)}
                  </Avatar>
                </ListItemAvatar>
                <ListItemText
                  primary={member.name}
                  secondary={
                    <Box>
                      <Typography variant="caption" display="block">
                        Katılım: {member.joinDate}
                      </Typography>
                      <Typography variant="caption" display="block">
                        Telegram: {member.telegramUsername}
                      </Typography>
                    </Box>
                  }
                />
                <ListItemSecondaryAction>
                  <Chip 
                    label={member.role}
                    size="small"
                    color={member.role === 'Moderator' ? 'primary' : 'default'}
                  />
                </ListItemSecondaryAction>
              </ListItem>
            ))}
          </List>
        </DialogContent>
        <DialogActions sx={{ p: 3 }}>
          <Button 
            startIcon={<PersonAdd />}
            sx={{ color: '#FF6B35' }}
          >
            Üye Ekle
          </Button>
          <Button onClick={() => setMemberDialogOpen(false)}>Kapat</Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
};

export default SportsGroups;
