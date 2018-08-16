/*
 * SharpDevelop tarafından düzenlendi.
 * Kullanıcı: abuzer
 * Tarih: 27.7.2018
 * Zaman: 19:31
 * 
 * Bu şablonu değiştirmek için Araçlar | Seçenekler | Kodlama | Standart Başlıkları Düzenle 'yi kullanın.
 */
using System;
using System.Timers;
namespace tank_oyunu
{

	class Program
	{
		const int boy=15;//haritanın boyutunu tutuan değişken
		static int hareketEttimi=0;//hareketettimi
		static int zaman=0;//kronometre
		static Timer t = new Timer(100);
		static int[,] kor=new int[boy,3];//botların kordinatı
		static int[,] korbomb=new int[boy,4];//0.ncı indis hangi tanka ait olduğu 1. indis x,2.indis y yi 3. indisi yönü
		static int[,] harita=new int[boy,boy];//haritamız
		static int x,z,x1,z1,yon,yon1;//x,z,yon tankın ;x1,z1,yon1 attığı bomabnın

		public static bool var_mi()//ben bomba atım mı?attıysam bi daha izin verme(patlayana kadar bekle)
		{
			// disable once ConvertToConstant.Local
			bool flag=true;
			for(int i=0;i<boy;i++)
			{
				for(int j=0;j<boy;j++)
				{
					if(harita[i,j]==9)
						return false;
				}
				
			}
			return flag;
		}
		public static void bot_bomba()//botların attığı bombabaları kontrol eder
		{
			// disable once LocalVariableHidesMember
			for(int x=0;x<boy;x++){//bütün botları dolas
				if(korbomb[x,0]==x)//bombanın patlamadıysa
				{
					switch(korbomb[x,3]){//bombanın yönü ne
						case 1:
							if(harita[korbomb[x,1]+1,korbomb[x,2]]==0){//bombanın istikametinde bişey yoksa ilerlet
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1]+=1,korbomb[x,2]]=5;
								
							}else if(harita[korbomb[x,1]+1,korbomb[x,2]]!=1)//1 varsa bombayı ve istikametindekini patlat
							{
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1]+1,korbomb[x,2]]=0;korbomb[x,0]=-1;//bomba yok olur
							}else{
								harita[korbomb[x,1],korbomb[x,2]]=0;korbomb[x,0]=-1;//yoksa kendini patlatsın
							}break;//4 yön için aynısını yazdık
							case 2:if(harita[korbomb[x,1],korbomb[x,2]-1]==0){
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1],korbomb[x,2]-=1]=5;
							}else if(harita[korbomb[x,1],korbomb[x,2]-1]!=1)
							{
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1],korbomb[x,2]-1]=0;korbomb[x,0]=-1;
							}else {
								harita[korbomb[x,1],korbomb[x,2]]=0;korbomb[x,0]=-1;
							}break;
							case 3:if(harita[korbomb[x,1],korbomb[x,2]+1]==0){
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1],korbomb[x,2]+=1]=5;
							}else if(harita[korbomb[x,1],korbomb[x,2]+1]!=1)
							{
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1],korbomb[x,2]+1]=0;korbomb[x,0]=-1;
							}else{
								harita[korbomb[x,1],korbomb[x,2]]=0;korbomb[x,0]=-1;
							}break;
							case 4:if(harita[korbomb[x,1]-1,korbomb[x,2]]==0){
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1]-=1,korbomb[x,2]]=5;
							}else if(harita[korbomb[x,1]-1,korbomb[x,2]]!=1)
							{
								harita[korbomb[x,1],korbomb[x,2]]=0;
								harita[korbomb[x,1]-1,korbomb[x,2]]=0;korbomb[x,0]=-1;
							}else{
								harita[korbomb[x,1],korbomb[x,2]]=0;korbomb[x,0]=-1;
							}
							break;
					}
				}
			}
		}
		private static void bastır(object o, ElapsedEventArgs a)
		{
			
			Console.Clear();
			bot_bomba();
			harita_yazdır();
		/*	for (int i = 0; i < boy; i++)
			{
				for (int j = 0; j < boy; j++)
				{
					if (harita[i, j]== 2)//ben haritanın neresindeydim
					{
						x = i;
						z = j;
						break;
					}
				}
			}*/
			
			if(zaman%5==0)
			{
				bot();
			}
			if(zaman>hareketEttimi*5){
				hareketEttimi++;
				switch (Console.ReadKey(true).KeyChar)
				{
					case '2':
						if (harita[x + 1, z] == 0)
						{harita[x,z]=0;
							yon=harita[x + 1, z] = 2;
							x++;
						}
						break;

					case '4':
						if (harita[x, z - 1] == 0)
						{
							harita[x,z]=0;
							yon=harita[x, z - 1] = 4;
							z--;
						}
						break;
					case '5':
						if(var_mi()){
							switch(yon){
									case 2:if(harita[x+1,z]!=1){
										harita[x1=x+1,z1=z]=9;yon1=2;
									}break;
									case 4:if(harita[x,z1-1]!=1){
										harita[x1=x,z1=z-1]=9;yon1=4;
									}break;
									case 6:if(harita[x,z+1]!=1){
										harita[x1=x,z1=z+1]=9;yon1=6;
									}break;
									case 8:if(harita[x-1,z]!=1){
										harita[x1=x-1,z1=z]=9;yon1=8;
									}break;
							}
						}else{
							
						}break;
					case '6':
						if (harita[x, z + 1] == 0)
						{
							harita[x,z]=0;
							yon=harita[x, z + 1] = 6;
							z++;
						}
						break;

					case '8':
						if (harita[x - 1, z] == 0)
						{
							harita[x,z]=0;
							yon=harita[x - 1, z] = 8;
							x--;
						}
						break;
				}
			}
			if(!var_mi())
			{
				switch(yon1){
					case 2:
						if(harita[x1+1,z1]==0){
							harita[x1,z1]=0;
							harita[x1+=1,z1]=9;
							
						}else if(harita[x1+1,z1]!=1)
						{
							harita[x1,z1]=0;
							harita[x1+1,z1]=0;
						}else harita[x1,z1]=0;break;
						case 4:if(harita[x1,z1-1]==0){
							harita[x1,z1]=0;
							harita[x1,z1-=1]=9;
						}else if(harita[x1,z1-1]!=1)
						{
							harita[x1,z1]=0;
							harita[x1,z1-1]=0;
						}else harita[x1,z1]=0;break;
						case 6:if(harita[x1,z1+1]==0){
							harita[x1,z1]=0;
							harita[x1,z1+=1]=9;
						}else if(harita[x1,z1+1]!=1)
						{
							harita[x1,z1]=0;
							harita[x1,z1+1]=0;
						}else harita[x1,z1]=0;break;
						case 8:if(harita[x1-1,z1]==0){
							harita[x1,z1]=0;
							harita[x1-=1,z1]=9;
						}else if(harita[x1-1,z1]!=1)
						{
							harita[x1,z1]=0;
							harita[x1-1,z1]=0;
						}else harita[x1,z1]=0;
						break;
				}
			}
			
			
			
		}
		public static void bot()
		{
			Random r=new Random();
			// disable once LocalVariableHidesMember
			int t=0;
			
			for(int i=0;i<boy;i++)
			{
				for(int j=0;j<boy;j++)
				{
					if(harita[i,j]==7)
					{
						kor[t,0]=i;
						kor[t,1]=j;
						t++;
					}
				}
			}
			
			
			for(int l=0;l<t;l++){
				switch (r.Next(1,6))
				{
					case 1:
						if (harita[kor[l,0] + 1, kor[l,1]] == 0)
						{
							harita[kor[l,0],kor[l,1]]=0;
							harita[kor[l,0] + 1, kor[l,1]] = 7;
							kor[l,2]=1;
						}
						break;

					case 2:
						if (harita[kor[l,0], kor[l,1] - 1] == 0)
						{
							harita[kor[l,0],kor[l,1]]=0;
							harita[kor[l,0], kor[l,1] - 1] = 7;
							kor[l,2]=2;
						}
						break;
					case 3:
						if (harita[kor[l,0], kor[l,1] + 1] == 0)
						{
							harita[kor[l,0],kor[l,1]]=0;
							harita[kor[l,0], kor[l,1] + 1] = 7;
							kor[l,2]=3;
						}
						break;

					case 4:
						if (harita[kor[l,0] - 1, kor[l,1]] == 0)
						{
							harita[kor[l,0],kor[l,1]]=0;
							harita[kor[l,0] - 1, kor[l,1]] =7;
							kor[l,2]=4;
						}
						break;
					case 5:
						if(korbomb[l,0]!=l){
							
							switch(kor[l,2]){
									case 1:if(harita[kor[l,0]+1,kor[l,1]]!=1){
										harita[korbomb[l,1]=kor[l,0]+1,korbomb[l,2]=kor[l,1]]=5;korbomb[l,0]=l;korbomb[l,3]=kor[l,2];
									}break;
									case 2:if(harita[kor[l,0],kor[l,1]-1]!=1){
										harita[korbomb[l,1]=kor[l,0],korbomb[l,2]=kor[l,1]-1]=5;korbomb[l,0]=l;korbomb[l,3]=kor[l,2];
									}break;
									case 3:if(harita[kor[l,0],kor[l,1]+1]!=1){
										harita[korbomb[l,1]=kor[l,0],korbomb[l,2]=kor[l,1]+1]=5;korbomb[l,0]=l;korbomb[l,3]=kor[l,2];
									}break;
									case 4:if(harita[kor[l,0]-1,kor[l,1]]!=1){
										harita[korbomb[l,1]=kor[l,0]-1,korbomb[l,2]=kor[l,1]]=5;korbomb[l,0]=l;korbomb[l,3]=kor[l,2];
									}break;
							}	
						}break;
				}
			}
		}
		public static void harita_bas()
		{
			Random r=new Random();
			for(int i=0;i<boy;i++)
			{
				for(int j=0;j<boy;j++)
				{
					if(i==0||i==boy-1||j==0||j==boy-1)
					{
						harita[i,j]=1;
					}else
						harita[i,j]=0;
				}
			}
			for(int i=0;i<40;i++){
				harita[r.Next(1,boy-1),r.Next(1,boy-1)]=10;
			}
			for(int i=0;i<7;i++){
				harita[r.Next(1,boy-1),r.Next(1,boy-1)]=7;
				
			}
			harita[x=r.Next(1,boy-1),z=r.Next(1,boy-1)]=2;
		}
		public static void harita_yazdır()
		{
			zaman+=1;
			for(int i=0;i<boy;i++)
			{
				for(int j=0;j<boy;j++)
				{
					if (harita[i, j] == 1)
						Console.Write("*");
					else
						switch (harita[i, j]) {
						case 0:
							Console.Write(" ");
							break;
						case 2:
							Console.Write("v");
							break;
						case 4:
							Console.Write("<");
							break;
						case 5:
							Console.Write(",");
							break;
						case 6:
							Console.Write(">");
							break;
						case 7:
							Console.Write("+");
							break;
						case 8:
							Console.Write("^");
							break;
							case 9:Console.Write(".");
							break;
						case 10:
							Console.Write("#");
							break;
					}
					
					
				}
				Console.WriteLine("");
			}
			Console.Write(zaman.ToString());
		}
		// disable once FunctionNeverReturns
		public static void Main(string[] args)
		{
			for(int i=0;i<boy;i++){
				korbomb[i,0]=-1;
				korbomb[i,1]=0;
				korbomb[i,2]=0;
				korbomb[i,3]=1;
			}
			harita_bas();
			harita_yazdır();
			
			t.Elapsed += new ElapsedEventHandler(Program.bastır);
			t.Start();
			while (true)
			{		
			}
			
		}
	}
}
