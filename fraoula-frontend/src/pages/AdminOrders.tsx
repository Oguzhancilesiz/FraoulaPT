import React, { useState } from 'react';
import {
  Container,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  Box,
  Chip,
  Button,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  Grid,
  Card,
  CardContent,
  LinearProgress
} from '@mui/material';
import {
  Visibility,
  Edit,
  LocalShipping,
  CheckCircle,
  Cancel,
  Assessment,
  ShoppingCart,
  LocalOffer,
  TrendingUp
} from '@mui/icons-material';

const AdminOrders = () => {
  const [selectedOrder, setSelectedOrder] = useState<any>(null);
  const [dialogOpen, setDialogOpen] = useState(false);

  // Örnek sipariş verileri
  const orders = [
    {
      id: 1,
      orderNumber: 'FRP-2024-001',
      customerName: 'Ahmet Yılmaz',
      customerEmail: 'ahmet@email.com',
      totalAmount: 549.98,
      status: 'Shipped',
      orderDate: '2024-01-15',
      shippedDate: '2024-01-16',
      trackingNumber: 'YK123456789',
      cargoCompany: 'Yurtiçi Kargo',
      items: [
        { name: 'FraoulaPT Signature Tişört', quantity: 2, price: 199.99 },
        { name: 'Premium Whey Protein', quantity: 1, price: 150.00 }
      ]
    },
    {
      id: 2,
      orderNumber: 'FRP-2024-002',
      customerName: 'Zeynep Kaya',
      customerEmail: 'zeynep@email.com',
      totalAmount: 849.97,
      status: 'Processing',
      orderDate: '2024-01-16',
      items: [
        { name: 'FraoulaPT Hoodie', quantity: 1, price: 349.99 },
        { name: 'Resistance Bands Set', quantity: 1, price: 179.99 },
        { name: 'BCAA Energy Drink', quantity: 4, price: 89.99 }
      ]
    },
    {
      id: 3,
      orderNumber: 'FRP-2024-003',
      customerName: 'Mehmet Demir',
      customerEmail: 'mehmet@email.com',
      totalAmount: 199.99,
      status: 'Delivered',
      orderDate: '2024-01-14',
      shippedDate: '2024-01-15',
      deliveredDate: '2024-01-17',
      trackingNumber: 'PTT987654321',
      cargoCompany: 'PTT Kargo',
      items: [
        { name: 'Pro Gym Eldivenleri', quantity: 1, price: 149.99 }
      ]
    }
  ];

  // İstatistikler
  const stats = [
    {
      title: 'Toplam Sipariş',
      value: '127',
      icon: <ShoppingCart />,
      color: '#FF6B35',
      trend: '+15%'
    },
    {
      title: 'Bekleyen Siparişler',
      value: '23',
      icon: <LocalOffer />,
      color: '#FFA500',
      trend: '+8%'
    },
    {
      title: 'Günlük Satış',
      value: '₺12,450',
      icon: <TrendingUp />,
      color: '#27AE60',
      trend: '+22%'
    },
    {
      title: 'Ortalama Sepet',
      value: '₺420',
      icon: <Assessment />,
      color: '#9B59B6',
      trend: '+5%'
    }
  ];

  const getStatusColor = (status: string) => {
    switch (status) {
      case 'Pending': return 'warning';
      case 'Processing': return 'info';
      case 'Shipped': return 'primary';
      case 'Delivered': return 'success';
      case 'Cancelled': return 'error';
      default: return 'default';
    }
  };

  const getStatusText = (status: string) => {
    switch (status) {
      case 'Pending': return 'Beklemede';
      case 'Processing': return 'Hazırlanıyor';
      case 'Shipped': return 'Kargoda';
      case 'Delivered': return 'Teslim Edildi';
      case 'Cancelled': return 'İptal Edildi';
      default: return status;
    }
  };

  const handleViewOrder = (order: any) => {
    setSelectedOrder(order);
    setDialogOpen(true);
  };

  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      <Typography variant="h4" sx={{ mb: 4, fontWeight: 700, color: '#FF6B35' }}>
        📦 Sipariş Yönetimi
      </Typography>

      {/* İstatistikler */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        {stats.map((stat, index) => (
          <Grid xs={12} sm={6} md={3} key={index}>
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <Box
                    sx={{
                      bgcolor: stat.color,
                      color: 'white',
                      borderRadius: 2,
                      p: 1,
                      mr: 2
                    }}
                  >
                    {stat.icon}
                  </Box>
                  <Box>
                    <Typography variant="h5" component="div">
                      {stat.value}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {stat.title}
                    </Typography>
                  </Box>
                </Box>
                <Chip 
                  label={stat.trend} 
                  size="small" 
                  color="success" 
                  variant="outlined" 
                />
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      {/* Sipariş Tablosu */}
      <Paper sx={{ borderRadius: 3, overflow: 'hidden' }}>
        <Box sx={{ p: 3, bgcolor: '#F8F9FA', borderBottom: '1px solid #E0E0E0' }}>
          <Typography variant="h6" sx={{ fontWeight: 600 }}>
            Son Siparişler
          </Typography>
        </Box>
        
        <TableContainer>
          <Table>
            <TableHead>
              <TableRow sx={{ bgcolor: '#FFF5F0' }}>
                <TableCell sx={{ fontWeight: 600 }}>Sipariş No</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Müşteri</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Tutar</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Durum</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Tarih</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>Kargo</TableCell>
                <TableCell sx={{ fontWeight: 600 }}>İşlemler</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {orders.map((order) => (
                <TableRow key={order.id} sx={{ '&:hover': { bgcolor: '#FFFAF5' } }}>
                  <TableCell>
                    <Typography variant="body2" sx={{ fontWeight: 600, color: '#FF6B35' }}>
                      {order.orderNumber}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Box>
                      <Typography variant="body2" sx={{ fontWeight: 500 }}>
                        {order.customerName}
                      </Typography>
                      <Typography variant="caption" color="text.secondary">
                        {order.customerEmail}
                      </Typography>
                    </Box>
                  </TableCell>
                  <TableCell>
                    <Typography variant="body2" sx={{ fontWeight: 600 }}>
                      ₺{order.totalAmount.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    <Chip 
                      label={getStatusText(order.status)}
                      color={getStatusColor(order.status) as any}
                      size="small"
                      variant="outlined"
                    />
                  </TableCell>
                  <TableCell>
                    <Typography variant="body2">
                      {new Date(order.orderDate).toLocaleDateString('tr-TR')}
                    </Typography>
                  </TableCell>
                  <TableCell>
                    {order.trackingNumber ? (
                      <Box>
                        <Typography variant="body2" sx={{ fontWeight: 500 }}>
                          {order.trackingNumber}
                        </Typography>
                        <Typography variant="caption" color="text.secondary">
                          {order.cargoCompany}
                        </Typography>
                      </Box>
                    ) : (
                      <Typography variant="body2" color="text.secondary">
                        Henüz kargoya verilmedi
                      </Typography>
                    )}
                  </TableCell>
                  <TableCell>
                    <Box sx={{ display: 'flex', gap: 1 }}>
                      <IconButton 
                        size="small" 
                        onClick={() => handleViewOrder(order)}
                        sx={{ color: '#FF6B35' }}
                      >
                        <Visibility />
                      </IconButton>
                      <IconButton size="small" sx={{ color: '#27AE60' }}>
                        <Edit />
                      </IconButton>
                      {order.status === 'Processing' && (
                        <IconButton size="small" sx={{ color: '#3498DB' }}>
                          <LocalShipping />
                        </IconButton>
                      )}
                    </Box>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>

      {/* Sipariş Detay Dialog */}
      <Dialog 
        open={dialogOpen} 
        onClose={() => setDialogOpen(false)}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle sx={{ bgcolor: '#FF6B35', color: 'white' }}>
          Sipariş Detayları - {selectedOrder?.orderNumber}
        </DialogTitle>
        <DialogContent sx={{ p: 3 }}>
          {selectedOrder && (
            <Grid container spacing={3}>
              <Grid xs={12} md={6}>
                <Typography variant="h6" gutterBottom>
                  Müşteri Bilgileri
                </Typography>
                <Typography><strong>Ad:</strong> {selectedOrder.customerName}</Typography>
                <Typography><strong>E-posta:</strong> {selectedOrder.customerEmail}</Typography>
                <Typography><strong>Sipariş Tarihi:</strong> {new Date(selectedOrder.orderDate).toLocaleDateString('tr-TR')}</Typography>
              </Grid>
              <Grid xs={12} md={6}>
                <Typography variant="h6" gutterBottom>
                  Kargo Bilgileri
                </Typography>
                {selectedOrder.trackingNumber ? (
                  <>
                    <Typography><strong>Takip No:</strong> {selectedOrder.trackingNumber}</Typography>
                    <Typography><strong>Kargo Şirketi:</strong> {selectedOrder.cargoCompany}</Typography>
                    {selectedOrder.shippedDate && (
                      <Typography><strong>Kargo Tarihi:</strong> {new Date(selectedOrder.shippedDate).toLocaleDateString('tr-TR')}</Typography>
                    )}
                  </>
                ) : (
                  <Typography color="text.secondary">Henüz kargoya verilmedi</Typography>
                )}
              </Grid>
              <Grid xs={12}>
                <Typography variant="h6" gutterBottom>
                  Sipariş İçeriği
                </Typography>
                <TableContainer component={Paper} variant="outlined">
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Ürün</TableCell>
                        <TableCell align="right">Adet</TableCell>
                        <TableCell align="right">Birim Fiyat</TableCell>
                        <TableCell align="right">Toplam</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {selectedOrder.items.map((item: any, index: number) => (
                        <TableRow key={index}>
                          <TableCell>{item.name}</TableCell>
                          <TableCell align="right">{item.quantity}</TableCell>
                          <TableCell align="right">₺{item.price.toFixed(2)}</TableCell>
                          <TableCell align="right">₺{(item.quantity * item.price).toFixed(2)}</TableCell>
                        </TableRow>
                      ))}
                      <TableRow>
                        <TableCell colSpan={3} sx={{ fontWeight: 600 }}>Toplam</TableCell>
                        <TableCell align="right" sx={{ fontWeight: 600 }}>
                          ₺{selectedOrder.totalAmount.toFixed(2)}
                        </TableCell>
                      </TableRow>
                    </TableBody>
                  </Table>
                </TableContainer>
              </Grid>
            </Grid>
          )}
        </DialogContent>
      </Dialog>
    </Container>
  );
};

export default AdminOrders;
